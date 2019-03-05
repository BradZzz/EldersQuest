using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
 {
     int initialPointerId;

     Transform parent, toReturn;
 
     void Start()
     {
         initialPointerId = int.MaxValue;
         parent = toReturn = gameObject.transform.parent;
     }
 
     public void OnBeginDrag(PointerEventData eventData)
     {
         Debug.Log("OnBeginDrag");
         Debug.Log("Transform Parent: " + this.transform.parent.name);

         if (initialPointerId == int.MaxValue)
         {
             initialPointerId = eventData.pointerId;
         }
         //while (!this.transform.parent.name.Equals("Canvas")) {
         //    this.transform.parent = this.transform.parent.parent;
         //}
         //this.transform.parent = this.transform.parent.parent;
         //transform.parent = transform.root;
         transform.parent = transform.parent.parent;
     }
 
     public void OnEndDrag(PointerEventData eventData)
     {
        Debug.Log("OnEndDrag: " + parent.name);
        SetParent(parent);
        if (initialPointerId == eventData.pointerId)
        {
           initialPointerId = int.MaxValue;
        }
        TechTreeNav.instance.SavePosition();
        TechTreeNav.instance.RefreshSelect();
     }
 
     public void OnDrag(PointerEventData eventData)
     {
         if (initialPointerId == eventData.pointerId)
         {
             this.transform.position = eventData.position;
         }
     }

     public void SetParent(Transform parent){
        Debug.Log("Parent Set To: " + parent.name);
        this.parent = parent;
        this.transform.parent = this.parent;
        this.transform.parent.gameObject.SetActive(false);
        this.transform.parent.gameObject.SetActive(true);
        //LayoutRebuilder.ForceRebuildLayoutImmediate(this.toReturn.GetComponent<RectTransform>());
     }
  
     public Transform GetParent(){
        return parent;
     }

     public void SetToReturn(Transform toReturn){
        Debug.Log("toReturn Set To: " + toReturn.name);
        this.toReturn = toReturn;
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.parent.GetComponent<RectTransform>());
     }
  
     public Transform GetToReturn(){
        return toReturn;
     }
}