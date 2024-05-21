using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ZXing;



public class LogIn : MonoBehaviour
{
    public Button loginButton;
    public TMP_InputField loginFormEmail;
    public TMP_InputField loginFormPassword;
    public void CallLogin() 
    {
        StartCoroutine(UserLogin());
    }
   IEnumerator UserLogin()
   {
        WWWForm form = new WWWForm();
        form.AddField("userEmail",loginFormEmail.text);
        form.AddField("userPassword",loginFormPassword.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php",form);
        yield return www.SendWebRequest();
        
        string result = www.downloadHandler.text;
        Debug.Log(result);
        if(result[0] == '0')
        {
            DBManager.email = loginFormEmail.text;
            DBManager.wishExCode= result.Split('\t')[1];
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        }
        else
        {
            
            Debug.Log(result);
        }  

        www.Dispose();
   }

   public void VerifyInputs()
   {
    loginButton.interactable=(loginFormEmail.text.Contains('@') && loginFormEmail.text.Contains('.'));
   }
}
