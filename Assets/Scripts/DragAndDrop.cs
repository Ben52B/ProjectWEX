using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,IEndDragHandler,IDragHandler,IDataPresistence 
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
            animation.name =this.name;
            this.name=this.name+"_used";
            animation.SetParent(canvas);
            animation.transform.localScale = new Vector3(1,1,1);
            this.GetComponent<ParticleSystem>().Play();

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
    
 

public void LoadData(WishData data)
{
    this.rectTransform.position = data.objectPosition;
}

public void SaveData(ref WishData data)
{
    Debug.Log(this.transform.position);
    data.objectPosition= this.transform.position;
}



}