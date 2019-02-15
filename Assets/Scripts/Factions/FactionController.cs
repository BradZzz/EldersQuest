using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactionController : MonoBehaviour
{
    public GameObject btn;
    public TextMeshProUGUI desc;

    // Start is called before the first frame update
    void Awake()
    {
        btn.SetActive(false);
        desc.text = "";
    }

    // Update is called once per frame
    public void Click(string faction)
    {
        PlayerMeta player = BaseSaver.GetPlayer();
        player.faction = (Unit.FactionType)Enum.Parse(typeof(Unit.FactionType), faction);
        BaseSaver.PutPlayer(player);
        desc.text = faction + ": "+ Unit.GetFactionDesc(player.faction);
        btn.SetActive(true);
    }
}
