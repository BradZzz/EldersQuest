using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData) {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        Debug.Log("Entered: " + eventData.pointerDrag.name);
        if (d != null)
        {
            d.SetParent(this.transform);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.GetParent() == this.transform)
        {
            d.SetParent(d.GetToReturn());
        }
    }

    public void OnDrop(PointerEventData eventData) {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        Debug.Log(eventData.pointerDrag.name + " dropped on " + d.GetParent().name);
        if (d != null) {
            d.SetToReturn(this.transform);
        }
    }
}
