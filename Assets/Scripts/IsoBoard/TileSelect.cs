using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
    public GameObject glossary;
    public Vector2 pos;


    private bool waiting = false;
    private IEnumerator waitIE = null;
    //private bool active = false;
    private Vector3 restingPos;

    ///these should be changed to be more generic rather than having a gameobject for every type of tile object
    private GameObject unit;
    private GameObject highlight;
    private GameObject attack;
    private List<TileSelect> cursors;

    private IEnumerator WaitForClick()
    {
        waiting = true;
        yield return new WaitForSeconds(.1f);
        waiting = false;
    }

    private void Awake()
    {
        cursors = new List<TileSelect>();
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
            if (t.gameObject.name.Equals("Attack"))
            {
                attack = t.gameObject;
            }
        }
    }

    public TileSelect[] GetCursors()
    {
        return cursors.ToArray();
    }

    public void ClearCursors()
    {
        cursors.Clear();
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

    public void Deactivate()
    {
        //active = false;
        transform.position = restingPos;
        highlight.SetActive(false);
        unit.SetActive(false);
        attack.SetActive(false);
        //foreach (TileSelect surrT in cursors)
        //{
        //  surrT.Deactivate();
        //}
    }

    public void Attack()
    {
        //active = false;
        //transform.position = restingPos;
        attack.SetActive(true);
        //unit.SetActive(false);
    }

    public void NotAttack()
    {
        //active = false;
        //transform.position = restingPos;
        attack.SetActive(false);
        //unit.SetActive(false);
    }

    public void Select()
    {
        //active = false;
        transform.position = restingPos;
        highlight.SetActive(true);
        //unit.SetActive(false);
    }

    public void Deselect()
    {
        //active = false;
        //transform.position = restingPos;
        highlight.SetActive(false);
        //attack.SetActive(false);
        //unit.SetActive(false);
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

        //TileSelect[] surrTiles = BoardCoordinator.instance.GetSurroundingTiles(from.gameObject.GetComponent<TileSelect>());
        //foreach (TileSelect surrT in surrTiles)
        //{
        //  if (surrT != null && !surrT.ContainsCharacter())
        //  {
        //    surrT.Deactivate();
        //  }
        //}

        foreach (TileSelect surrT in from.gameObject.GetComponent<TileSelect>().GetCursors())
        {
            if (surrT != null && !surrT.ContainsCharacter())
            {
                surrT.Deactivate();
            }
        }
        from.gameObject.GetComponent<TileSelect>().ClearCursors();

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
        //active = true;
        ActivateTile(BoardCoordinator.instance.GetChar(this), Character.dirs.S);
    }

    public void Activate(Transform from)
    {
        //active = true;
        Character.dirs position = BoardCoordinator.instance.SetCurrPos(from, transform);
        BoardCoordinator.instance.PutChar(from.GetComponent<TileSelect>(), transform.GetComponent<TileSelect>(),
          BoardCoordinator.instance.GetChar(from.GetComponent<TileSelect>()));
        StartCoroutine(MoveCharacter(from, transform, position, BoardCoordinator.instance.GetChar(this)));
    }

    void ActivateTile(Character character, Character.dirs position)
    {
        unit.SetActive(true);
        if (position != Character.dirs.None)
        {
            unit.GetComponent<SpriteRenderer>().sprite = character.GetDirectionalSprite(position);
        }
        TileSelect[] surrTiles = BoardCoordinator.instance.GetSurroundingTiles(this);
        foreach (TileSelect surrT in surrTiles)
        {
            cursors.Add(surrT);
            surrT.Select();
        }
    }

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
        StartCoroutine(UpdateEvents());
    }

    IEnumerator UpdateEvents()
    {
        if (this == BoardCoordinator.instance.GetSelected())
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            SelectTiles(cursors.ToArray());
            //if (!SelectTiles(cursors.ToArray()))
            //{
            //  NotAttack();
            //}
        }
        else if (GetComponent<TileSelect>().ContainsCharacter())
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            //SelectTiles(GetComponent<TileSelect>().GetCursors());
            if (!SelectTiles(GetComponent<TileSelect>().GetCursors()))
            {
                NotAttack();
            }
        }
        else if (GetComponent<TileSelect>().GetHiglight().activeInHierarchy)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        yield return null;
    }

    bool SelectTiles(TileSelect[] tls)
    {
        bool charsAround = false;
        foreach (TileSelect tl in tls)
        {
            if (!tl.ContainsCharacter())
            {
                tl.Select();
                tl.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else
            {
                charsAround = true;
                tl.Deselect();
                if (this == BoardCoordinator.instance.GetSelected())
                {
                    tl.Attack();
                }
                else
                {
                    tl.NotAttack();
                }
            }
        }
        return charsAround;
    }

    IEnumerator EvaluateHit(Ray ray)
    {
        yield return null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, Vector2.down);
        if (hits.Length > 0)
        {
            BoardCoordinator.instance.PutSelected(hits[0].transform.gameObject.GetComponent<TileSelect>());
        }
    }
}
