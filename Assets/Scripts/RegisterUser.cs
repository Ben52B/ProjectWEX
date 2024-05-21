using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class RegisterUser : MonoBehaviour
{
    public Button submitButton;
    public TMP_InputField formFirstName;
    public TMP_InputField formLastName;
    public TMP_InputField formEmail;
    public TMP_InputField formPassword;
    public static TextMeshProUGUI formWishExCode;
    

    public void CallRegister() 
    {
        StartCoroutine(CreateUser());
    }
   IEnumerator CreateUser()
   {
        WWWForm form = new WWWForm();
        form.AddField("userFirstName",formFirstName.text);
        form.AddField("userLastName",formLastName.text);
        form.AddField("userEmail",formEmail.text);
        form.AddField("userPassword",formPassword.text);
        form.AddField("userWishExCode",formWishExCode.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php",form);
        yield return www.SendWebRequest();
 

        if(www.result != UnityWebRequest.Result.Success)
        {
            
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;
            Debug.Log(results);
        }  

        www.Dispose();
   }

   public void VerifyInputs()
   {
    submitButton.interactable=(formFirstName.text.Length >=8 && formLastName.text.Length >=8);
   }
}
