using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.EventSystems;


public class WEXmanager : MonoBehaviour
{
    public float height = 1f;
    public float width = 1f;
    private float ratio = 0;
    private Vector3 PlaneScale;
    public float speed = 5;


    private void Update() {
        ratio= height/width;
        PlaneScale = new Vector3(height,1,width);
        this.transform.localScale = PlaneScale;

        
        if(Input.GetMouseButton(0))
        {
            
           // transform.Rotate(  Time.deltaTime * speed * new Vector3(Input.GetAxis("Mouse Y"),0,Input.GetAxis("Mouse X")) );

           transform.eulerAngles+= speed * new Vector3(Input.GetAxis("Mouse Y"),-1*Input.GetAxis("Mouse X"),0);
        }
    }
}
