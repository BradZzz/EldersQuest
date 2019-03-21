using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DynamicSorting : MonoBehaviour
{
    Renderer rend;
    void LateUpdate()
    {
        var renderer = GetComponent<Renderer>();
        renderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(renderer.bounds.min).y * -1 + 1000;
    }

    void Awake()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

}