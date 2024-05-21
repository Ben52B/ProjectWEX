using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using UnityEngine.UI;
using ZXing;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using ZXing.QrCode.Internal;

public class QRScan : MonoBehaviour
{
    [SerializeField] private RawImage rawImageBackground;
    [SerializeField] private AspectRatioFitter aspectRtioFitter;
    public TMP_Text textOut;
    private bool isCameraAvilable;
    private WebCamTexture cameraTexture;
    
   
    
    void Start()
    {
        SetupCamera();
    }

    void Update()
    {
        Scan();
        
    }
    void SetupCamera()
    {
        Debug.Log("im in setup camera");
        WebCamDevice[] devices = WebCamTexture.devices;
        if(devices.Length==0)
        {
            isCameraAvilable=false;
            Debug.Log("There is no Camera");
            return;
        }
        Debug.Log(devices.Length);
        for(int i=0; i<devices.Length; i++)
        {
            if(devices[i].isFrontFacing==true)
            {
                cameraTexture = new WebCamTexture(devices[i].name,Screen.width,Screen.height);
                Debug.Log(devices[i].name);
                
            }
        }
        isCameraAvilable=true;
        cameraTexture.Play();
        UpdateCameraRender();
        rawImageBackground.texture=cameraTexture;
        
    }
    void UpdateCameraRender()
    {
        if(isCameraAvilable==false || cameraTexture.isPlaying==false)
        {
            Debug.Log("Camera is not available");
            return;
        }
        float ratio = (float)cameraTexture.width/(float)cameraTexture.height;
        aspectRtioFitter.aspectRatio=ratio;

        int orientation = -cameraTexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles=new Vector3(0,0,orientation);
    }
    void Scan()
    {


         try
        {
            IBarcodeReader qrReader = new BarcodeReader();
            Result result = qrReader.Decode(cameraTexture.GetPixels32(),cameraTexture.width,cameraTexture.height);

            if(result!=null)
            {
                
                textOut.text=result.Text;
                cameraTexture.Stop();
                SceneManager.LoadScene("Register");
                
            }
            else
            {
                
                textOut.text="Faild to read QR code";
            }
        }
        catch (System.Exception)
        {
            
            textOut.text="Can't find QR";
        }
    }
}
