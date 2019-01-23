using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
  public GameObject glossary;


  private bool waiting = false;
  private IEnumerator waitIE = null;
  private bool active = false;
  public Vector2 pos;
  private Vector3 restingPos;

  private IEnumerator WaitForClick()
  {
    waiting = true;
    yield return new WaitForSeconds(.5f);
    waiting = false;
  }

  private void Awake()
  {
    restingPos = transform.position;
  }

  public Vector3 GetResting()
  {
    return restingPos;
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
    foreach (Transform t in transform)
    {
      t.gameObject.SetActive(false);
    }
  }

  public void Select()
  {
    active = false;
    transform.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    transform.position = restingPos;
    //transform.Find("Unit")
    //Debug.Log("Deactivating: " + transform.gameObject.name);
    foreach (Transform t in transform)
    {
      if (t.gameObject.name.Equals("Highlight"))
      {
        t.gameObject.SetActive(true);
      }
      else
      {
        t.gameObject.SetActive(false);
      }
    }
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
    GameObject unitFrom = FindNameInTransform(from, "Unit");
    GameObject unitTo = FindNameInTransform(to, "Unit");

    GameObject character = Instantiate(unitFrom, unitFrom.transform.position, Quaternion.identity, BoardCoordinator.instance.transform);
    Debug.Log("Position: " + position);
    character.GetComponent<SpriteRenderer>().sprite = BoardCoordinator.instance.glossary.GetComponent<Glossary>().characters[0].GetDirectionalSprite(position);
    //unit.SetActive(false);
    //iTween.MoveTo(character, unitTo.transform.position,.2f);
    iTween.MoveTo(character, iTween.Hash("x", unitTo.transform.position.x, "y", unitTo.transform.position.y, 
      "islocal", false, "time", .2f, "looptype", "none", "easetype", "linear"));
    yield return new WaitForSeconds(.2f);
    ActivateTile();
    Destroy(character);
  }

  public void Activate()
  {
    active = true;
    if (BoardCoordinator.instance.GetCurrentTile() != null && BoardCoordinator.instance.GetLastTile() != null)
    {
      TileSelect from = BoardCoordinator.instance.GetCurrentTile();

      //Character.dirs position = BoardCoordinator.instance.SetCurrPos(transform);

      //if (position != Character.dirs.None)
      //{
      //  Debug.Log("Received char direction: " + position.ToString());
      //  foreach (Transform t in transform)
      //  {
      //    if (t.gameObject.name.Equals("Unit"))
      //    {
      //      t.gameObject.GetComponent<SpriteRenderer>().sprite = BoardCoordinator.instance.glossary.GetComponent<Glossary>().characters[0].GetDirectionalSprite(position);
      //    }
      //  }
      //}

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
    Character.dirs position = BoardCoordinator.instance.SetCurrPos(transform, true);

    if (position != Character.dirs.None)
    {
      Debug.Log("Received char direction: " + position.ToString());
      foreach (Transform t in transform)
      {
        if (t.gameObject.name.Equals("Unit"))
        {
          t.gameObject.GetComponent<SpriteRenderer>().sprite = BoardCoordinator.instance.glossary.GetComponent<Glossary>().characters[0].GetDirectionalSprite(position);
        }
      }
    }

    transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    //Character.dirs position = Character.dirs.None;
    //position = BoardCoordinator.instance.SetCurrPos(transform);

    foreach (Transform t in transform)
    {
      if (t.gameObject.name.Equals("Unit"))
      {
        t.gameObject.SetActive(true);
      }
    }

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
          //Debug.Log("Hits: " + hits.Length.ToString());
          foreach (Transform child in GameObject.Find("Tilemap").transform)
          {
            //child.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            Deactivate();
          }
          if (hits[0])
          {
            hits[0].transform.gameObject.GetComponent<TileSelect>().Activate();
          }
        }
      }
    }
  }
}
