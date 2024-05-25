using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private RectTransform canvas;
 
    private RectTransform rectTransform;
    private void Awake() 
    {
       rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        if (!this.name.Contains("_used") )
        {
            Transform animation = Instantiate(this.transform, this.transform.position,this.transform.rotation);
            this.name=animation.name+"_used";
            animation.SetParent(canvas);
            animation.transform.localScale = new Vector3(1,1,1);

        }

        //Debug.Log(this.GetComponent<RectTransform>().anchoredPosition);
        //animation.GetComponent<RectTransform>().anchoredPosition=this.GetComponent<RectTransform>().anchoredPosition;
        //animation.transform.position = this.transform.position;
        
        
        //Debug.Log(animation.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    
 }

