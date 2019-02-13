//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;
//using System.Collections;

//public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

//    [HideInInspector]
//    public Transform parentToReturnTo = null;
//    [HideInInspector]
//    public Transform placeHolderParent = null;

//    private GameObject placeHolder = null;

//    public void OnBeginDrag(PointerEventData eventData) {
//        placeHolder = new GameObject();
//        placeHolder.transform.SetParent( this.transform.parent );
//        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
//        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
//        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
//        le.flexibleWidth = 0;
//        le.flexibleHeight = 0;

//        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex() );

//        parentToReturnTo = this.transform.parent;
//        placeHolderParent = parentToReturnTo;
//        this.transform.SetParent(this.transform.parent.parent);

//        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
//    }

//    public void OnDrag(PointerEventData eventData) {
//        if (placeHolderParent == null) {
      
//        }

//        this.transform.position = eventData.position;

//        if (placeHolder.transform.parent != placeHolderParent)
//        {
//            placeHolder.transform.SetParent(placeHolderParent);
//        }
        
//        int newSiblingIndex = placeHolderParent.childCount;

//        for (int i = 0; i < placeHolderParent.childCount; i++)
//        {
//            if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
//            {
//                newSiblingIndex = i;

//                if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
//                    newSiblingIndex--;
                
//                break;
//            }
//        }

//        placeHolder.transform.SetSiblingIndex(newSiblingIndex);
//    }

//    public void OnEndDrag(PointerEventData eventData) {
//        this.transform.SetParent(parentToReturnTo);
//        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
//        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
//        Destroy(placeHolder);
//    }
//}

 using System;
 using UnityEngine;
 using UnityEngine.EventSystems;
     
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
         if (initialPointerId == int.MaxValue)
         {
             initialPointerId = eventData.pointerId;
         }
     }
 
     public void OnEndDrag(PointerEventData eventData)
     {
         if (initialPointerId == eventData.pointerId)
         {
             initialPointerId = int.MaxValue;
         }
     }
 
     public void OnDrag(PointerEventData eventData)
     {
         if (initialPointerId == eventData.pointerId)
         {
             // DO WHATEVER YOU WANNA DO DURING DRAGGING HERE
             this.transform.position = eventData.position;
         }
     }

     public void SetParent(Transform parent){
        this.parent = parent;
        this.transform.parent = this.parent;
     }
  
     public Transform GetParent(){
        return parent;
     }

     public void SetToReturn(Transform toReturn){
        this.toReturn = toReturn;
     }
  
     public Transform GetToReturn(){
        return toReturn;
     }
}