using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechTreeUnitWrap : MonoBehaviour
{
    public Unit unit;

    public void Refresh(){
        if (unit.GetLvl() >= unit.GetCurrentClass().GetWhenToUpgrade()) {
            GetComponent<Outline>().effectColor = Color.red;
            GetComponent<Outline>().effectDistance = new Vector2(5,-5);
        } else {
            GetComponent<Outline>().effectColor = Color.black;
            GetComponent<Outline>().effectDistance = new Vector2(1,-1);
        }
    }
}
