using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {

    public float scrollSpeed;
    public bool vertical;
    public bool horizontal;

    public bool bob;

    public float staticXOffset;
    public float staticYOffset;

    public float minOffsetX;  
    public float minOffsetY;  

    public float maxOffset;

    private Vector2 savedOffset;

    void Start () {
        savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
        savedOffset.x = minOffsetX;
        savedOffset.y = minOffsetY;
    }

    void Update () {
        float time = Mathf.Repeat (Time.time * scrollSpeed, maxOffset);
        Vector2 offset = new Vector2 (savedOffset.x, savedOffset.y);
        if (horizontal) {
            offset.x = time * staticXOffset;
        }
        if (vertical) {
            offset.y = time * staticYOffset;
        }
        if (bob) {
            offset.y = Mathf.PingPong (Time.time * scrollSpeed, .05f);
        }
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
    }

    void OnDisable () {
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
    }
}