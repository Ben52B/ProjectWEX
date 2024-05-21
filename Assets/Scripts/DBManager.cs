using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    public static string email;
    public static string wishExCode;
    public static bool LoggedIn {get{return email !=null;}}
    public static void LogOut()
    {
        email=null;
    }
}
