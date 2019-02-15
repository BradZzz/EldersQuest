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
        } else {
            GetComponent<Outline>().effectColor = Color.black;
        }
    }
}
