using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("pointer click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer ENTER");
    }

    void OnMouseDown()
    {
        Debug.Log("pointer click");
    }
}
