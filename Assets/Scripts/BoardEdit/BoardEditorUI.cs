using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardEditorUI : MonoBehaviour
{
    public GameObject glossary;

    public Image selImg;

    public InputField heightInput;
    public InputField widthInput;

    public static BoardEditorUI instance;

    private int height;
    private int width;
    private TileEditTypes paintTile;

    void Awake (){
      widthInput.onValueChanged.AddListener((value) => SetWidth(value));
      heightInput.onValueChanged.AddListener((value) => SetHeight(value));
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

    public void Resize(){
        BoardEditProxy.instance.Resize(height, width);
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
