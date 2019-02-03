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

  public GameObject GetHiglight()
  {
    return highlight;
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

  public bool GetActive()
  {
    return active;
  }

  public void Deactivate()
  {
    active = false;
    //transform.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    transform.position = restingPos;
    //transform.Find("Unit")
    //Debug.Log("Deactivating: " + transform.gameObject.name);

    highlight.SetActive(false);
    unit.SetActive(false);

    //TileSelect[] surrTiles = BoardCoordinator.instance.GetSurroundingTiles(this);
    //foreach (TileSelect surrT in surrTiles)
    //{
    //  if (surrT!= null && !surrT.ContainsCharacter())
    //  {
    //    surrT.Deactivate();
    //  }
    //}
  }

  public void Select()
  {
    active = false;
    //transform.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    transform.position = restingPos;
    //transform.Find("Unit")
    //Debug.Log("Deactivating: " + transform.gameObject.name);

    highlight.SetActive(true);
    unit.SetActive(false);
  }

  public bool ContainsCharacter()
  {
    return unit.gameObject.activeInHierarchy;
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

  IEnumerator MoveCharacter(Transform from, Transform to, Character.dirs position, Character tileChar)
  {
    //BoardCoordinator.instance.PutChar(from.GetComponent<TileSelect>(), to.GetComponent<TileSelect>(), tileChar);

    GameObject unitFrom = from.gameObject.GetComponent<TileSelect>().GetUnit();
    GameObject unitTo = to.gameObject.GetComponent<TileSelect>().GetUnit();

    TileSelect[] surrTiles = BoardCoordinator.instance.GetSurroundingTiles(from.gameObject.GetComponent<TileSelect>());
    foreach (TileSelect surrT in surrTiles)
    {
      if (surrT != null && !surrT.ContainsCharacter())
      {
        surrT.Deactivate();
      }
    }
    //from.gameObject.GetComponent<TileSelect>().Deactivate();

    GameObject character = Instantiate(unitFrom, unitFrom.transform.position, Quaternion.identity, BoardCoordinator.instance.transform);
    foreach (Transform child in character.transform)
    {
      child.gameObject.SetActive(false);
    }

    from.gameObject.GetComponent<TileSelect>().Deactivate();

    Debug.Log("TlSelect");
    Debug.Log(character);
    Debug.Log(tileChar);

    if (position != Character.dirs.None)
    {
      character.GetComponent<SpriteRenderer>().sprite = tileChar.GetDirectionalSprite(position);
    }
    else
    {
      character.GetComponent<SpriteRenderer>().sprite = tileChar.GetDirectionalSprite(Character.dirs.S);
    }
    //foreach (Transform child in character.transform)
    //{
    //  if (child.name.Equals("Health")) {
    //    child.gameObject.SetActive(true);
    //  }
    //}

    iTween.MoveTo(character, iTween.Hash("x", unitTo.transform.position.x, "y", unitTo.transform.position.y - .05f, "z", unitTo.transform.position.z,
      "islocal", false, "time", .3f, "looptype", "none", "easetype", "linear"));
    iTween.ShakeRotation(character, iTween.Hash("z", 30f, "islocal", true, "delay", 0, "time", .25f));

    yield return new WaitForSeconds(.3f);
    ActivateTile(tileChar, position);
    Destroy(character);
  }

  /*
   * Moving from one tile to the next
   * 1) Figure out the last player clicked and store it in the boardCoordinator 
   */
  public void Activate()
  {
    active = true;
    ActivateTile(BoardCoordinator.instance.GetChar(this), Character.dirs.S);
  }

  public void Activate(Transform from)
  {
    active = true;
    Character.dirs position = BoardCoordinator.instance.SetCurrPos(from, transform);
    BoardCoordinator.instance.PutChar(from.GetComponent<TileSelect>(), transform.GetComponent<TileSelect>(), 
      BoardCoordinator.instance.GetChar(from.GetComponent<TileSelect>()));
    StartCoroutine(MoveCharacter(from, transform, position, BoardCoordinator.instance.GetChar(this)));
  }

  void ActivateTile(Character character, Character.dirs position)
  {
    unit.SetActive(true);

    //Character.dirs position = BoardCoordinator.instance.SetCurrPos(transform, true);

    if (position != Character.dirs.None)
    {
      unit.GetComponent<SpriteRenderer>().sprite = character.GetDirectionalSprite(position);
    }

    //transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

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
        StartCoroutine(EvaluateHit(ray));
      }
    }
    /*
     * Changes the tiles based on what's selected
     */
    if (this == BoardCoordinator.instance.GetSelected())
    {
      transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
    else if (GetHiglight().activeInHierarchy)
    {
      transform.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    else
    {
      transform.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
  }

  IEnumerator EvaluateHit(Ray ray)
  {
    yield return null;
    RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, Vector2.down);
    if (hits.Length > 0)
    {
      BoardCoordinator.instance.PutSelected(hits[0].transform.gameObject.GetComponent<TileSelect>());
      //hits[0].transform.gameObject.GetComponent<TileSelect>().Activate();
      //foreach (Transform child in GameObject.Find("Tilemap").transform)
      //{
      //  if (child != hits[0].transform)
      //  {
      //    if (!BoardCoordinator.instance.ContainsChar(child.GetComponent<TileSelect>()))
      //    {
      //      Deactivate();
      //    }
      //  }
      //}
    }
  }
}
