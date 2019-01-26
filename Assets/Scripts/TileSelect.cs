using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
  public GameObject glossary;
  public Vector2 pos;


  private bool waiting = false;
  private IEnumerator waitIE = null;
  private bool active = false;
  private Vector3 restingPos;

  private GameObject unit;
  private GameObject highlight;

  private IEnumerator WaitForClick()
  {
    waiting = true;
    yield return new WaitForSeconds(.1f);
    waiting = false;
  }

  private void Awake()
  {
    restingPos = transform.position;
    foreach (Transform t in transform)
    {
      if (t.gameObject.name.Equals("Unit"))
      {
        unit = t.gameObject;
      }
      if (t.gameObject.name.Equals("Highlight"))
      {
        highlight = t.gameObject;
      }
    }
  }

  public Vector3 GetResting()
  {
    return restingPos;
  }

  public GameObject GetUnit()
  {
    return unit;
  }

  public void SetPos(Vector2 pos)
  {
    this.pos = pos;
  }

  public void Deactivate()
  {
    active = false;
    transform.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    transform.position = restingPos;
    //transform.Find("Unit")
    //Debug.Log("Deactivating: " + transform.gameObject.name);

    highlight.SetActive(false);
    unit.SetActive(false);
  }

  public void Select()
  {
    active = false;
    transform.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    transform.position = restingPos;
    //transform.Find("Unit")
    //Debug.Log("Deactivating: " + transform.gameObject.name);

    highlight.SetActive(true);
    unit.SetActive(false);
  }

  static GameObject FindNameInTransform(Transform transform, string tName)
  {
    foreach (Transform t in transform)
    {
      if (t.gameObject.name.Equals(tName))
      {
        //t.gameObject.SetActive(true);
        return t.gameObject;
      }
    }
    return null;
  }

  IEnumerator MoveCharacter(Transform from, Transform to, Character.dirs position)
  {
    GameObject unitFrom = from.gameObject.GetComponent<TileSelect>().GetUnit();
    GameObject unitTo = to.gameObject.GetComponent<TileSelect>().GetUnit();

    GameObject character = Instantiate(unitFrom, unitFrom.transform.position, Quaternion.identity, BoardCoordinator.instance.transform);
    character.GetComponent<SpriteRenderer>().sprite = BoardCoordinator.instance.glossary.GetComponent<Glossary>().characters[0].GetDirectionalSprite(position);

    iTween.MoveTo(character, iTween.Hash("x", unitTo.transform.position.x, "y", unitTo.transform.position.y - .1f, "z", unitTo.transform.position.z,
      "islocal", false, "time", .3f, "looptype", "none", "easetype", "linear"));
    iTween.ShakeRotation(character, iTween.Hash("z", 30f, "islocal", true, "delay", 0, "time", .25f));

    yield return new WaitForSeconds(.3f);
    ActivateTile();
    Destroy(character);
  }

  public void Activate()
  {
    active = true;
    if (BoardCoordinator.instance.GetCurrentTile() != null && BoardCoordinator.instance.GetLastTile() != null)
    {
      TileSelect from = BoardCoordinator.instance.GetCurrentTile();

      Character.dirs position = BoardCoordinator.instance.SetCurrPos(transform, false);

      StartCoroutine(MoveCharacter(from.transform, transform, position));
    }
    else
    {
      ActivateTile();
    }
  }

  void ActivateTile()
  {
    unit.SetActive(true);

    Character.dirs position = BoardCoordinator.instance.SetCurrPos(transform, true);

    if (position != Character.dirs.None)
    {
      unit.GetComponent<SpriteRenderer>().sprite = BoardCoordinator.instance.glossary.GetComponent<Glossary>().characters[0].GetDirectionalSprite(position);
    }

    transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

    TileSelect[] surrTiles = BoardCoordinator.instance.GetSurroundingTiles(this);
    foreach (TileSelect surrT in surrTiles)
    {
      surrT.Select();
    }
  }

  //private void Start()
  //{
  //  Deactivate();
  //}

  // Update is called once per frame
  public void Update()
  {
    if (Input.GetMouseButtonUp(0))
    {
      if (!waiting)
      {
        if (waitIE != null)
        {
          StopCoroutine(waitIE);
        }

        waitIE = WaitForClick();
        StartCoroutine(waitIE);

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z)));

        //Debug.Log("GetMouseButtonDown Hit");

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, Vector2.down);
        if (hits.Length > 0)
        {
          if (hits[0])
          {
            hits[0].transform.gameObject.GetComponent<TileSelect>().Activate();
          }
          foreach (Transform child in GameObject.Find("Tilemap").transform)
          {
            Deactivate();
          }
        }
      }
    }
  }
}
