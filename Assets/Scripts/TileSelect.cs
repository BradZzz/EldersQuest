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
      t.gameObject.SetActive(false);
    }
  }

  public void Activate()
  {
    active = true;
    transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    Character.dirs position = Character.dirs.None;
    foreach (Transform t in transform)
    {
      t.gameObject.SetActive(true);
      if (t.gameObject.name.Equals("Unit"))
      {
        position = BoardCoordinator.instance.SetCurrPos(restingPos);
      }
    }

    //Character.dirs position = BoardCoordinator.instance.SetCurrPos(pos);
    if (position != Character.dirs.None)
    {
      Debug.Log("Received char direction: " + position.ToString());
      //transform.position = new Vector3(restingPos.x, restingPos.y + .05f, restingPos.z);

      foreach (Transform t in transform)
      {
        if (t.gameObject.name.Equals("Unit"))
        {
          t.gameObject.GetComponent<SpriteRenderer>().sprite = BoardCoordinator.instance.glossary.GetComponent<Glossary>().characters[0].GetDirectionalSprite(position);
        }
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
