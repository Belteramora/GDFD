using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllLight : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform _transform;
    public void OnDrag(PointerEventData eventData)
    {
        _transform.localPosition   += (Vector3)eventData.delta;
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
    }
    // Debug.Log(eventData.pointerCurrentRaycast.screenPosition);
    //_transform.localPosition = eventData.pointerCurrentRaycast.screenPosition;
    // _transform.localPosition = eventData.position;
}
