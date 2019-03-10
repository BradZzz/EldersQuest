using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoardEditorUI : MonoBehaviour
{
    public GameObject glossary;

    public Image selImg;

    public InputField nameInput;
    public InputField heightInput;
    public InputField widthInput;

    public static BoardEditorUI instance;

    private int height;
    private int width;
    private string mapName;
    private TileEditTypes paintTile;

    void Awake (){
      widthInput.onValueChanged.AddListener((value) => SetWidth(value));
      heightInput.onValueChanged.AddListener((value) => SetHeight(value));
      nameInput.onValueChanged.AddListener((value) => SetName(value));
      instance = this;
    }

    public void SetHeight(string height){
        Debug.Log("SetHeight: " + height);
        this.height = int.Parse(height);
    }

    public void SetWidth(string width){
        Debug.Log("SetWidth: " + width);
        this.width = int.Parse(width);
    }

    public void SetName(string mapName){
        Debug.Log("SetName: " + mapName);
        this.mapName = mapName;
    }

    public void Resize(){
        if (height > 0 && width > 0){
            BoardEditProxy.instance.Resize(height, width);
        }
    }

    public void SaveMap(){
        if (mapName.Length > 0) {
            BoardEditMeta bMeta = new BoardEditMeta(height, width, 
              BoardEditProxy.instance.GetUnits().Where(unt => unt.GetData().GetTeam() == 0).ToArray(),
              BoardEditProxy.instance.GetUnits().Where(unt => unt.GetData().GetTeam() == 1).ToArray(),
              BoardEditProxy.instance.GetSpecialTiles(TileEditTypes.fire).ToArray(),
              BoardEditProxy.instance.GetSpecialTiles(TileEditTypes.snow).ToArray(),
              BoardEditProxy.instance.GetSpecialTiles(TileEditTypes.wall).ToArray(),
              BoardEditProxy.instance.GetSpecialTiles(TileEditTypes.divine).ToArray()
            );

            BoardEditProxy.SaveItemInfo(mapName,JsonUtility.ToJson(bMeta));
        }
    }

    public void SetTile(int tile){
        Glossary glossy = glossary.GetComponent<Glossary>();
        switch((TileEditTypes)tile){
            case TileEditTypes.grass: selImg.sprite = glossy.grassTile; break;
            case TileEditTypes.fire: selImg.sprite = glossy.fireTile; break;
            case TileEditTypes.divine: selImg.sprite = glossy.divineTile; break;
            case TileEditTypes.wall: selImg.sprite = glossy.wallTile; break;
            case TileEditTypes.snow: selImg.sprite = glossy.snowTile; break;
            case TileEditTypes.player: selImg.sprite = glossy.playerTile.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite; break;
            case TileEditTypes.enemy: selImg.sprite = glossy.enemyTile.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite; break;
        }
        paintTile = (TileEditTypes) tile;
    }

    public TileEditTypes GetPaintTile(){
        return paintTile;
    }

    public Sprite GetPaintTileSprite(){
        return selImg.sprite;
    }

    public enum TileEditTypes{
        grass, fire, divine, wall, snow, player, enemy, none
    }
}
