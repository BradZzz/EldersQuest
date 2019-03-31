using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScene : MonoBehaviour
{
    public GameObject glossary;
    public TextMeshProUGUI headerTxtl;
    public Image faction;

    void Start(){
        PlayerMeta player = BaseSaver.GetPlayer();
        Glossary glossy = glossary.GetComponent<Glossary>();
        string[] endings = new string[]{ "Death has come..." };
        switch(player.faction){
          case Unit.FactionType.Human: 
            faction.sprite = glossy.humanFaction; 
            endings = StoryStatic.HUMAN_DEATHS;
            break;
          case Unit.FactionType.Cthulhu: 
            faction.sprite = glossy.chtulhuFaction; 
            endings = StoryStatic.CTHULHU_DEATHS;
            break;
          case Unit.FactionType.Egypt: 
            faction.sprite = glossy.egyptFaction; 
            endings = StoryStatic.EGYPT_DEATHS;
            break;
        }
        GetComponent<Image>().color = HelperScripts.GetColorByFaction(player.faction);
        HelperScripts.Shuffle(endings);
        headerTxtl.text = endings[0];
    }

    // Start is called before the first frame update
    public void ResetSave()
    {
        HighScoreMeta.SaveCurrentScore();
        BaseSaver.ResetAtSave();
        SceneManager.LoadScene("MainScene");
    }
}
