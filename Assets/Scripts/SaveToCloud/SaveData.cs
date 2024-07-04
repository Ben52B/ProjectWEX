using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using JetBrains.Annotations;


[FirestoreData]
public class SaveData
{
    /*
    public List<GameObject> objectsToSave;
    foreach (GameObject obj in objectsToSave)
    {
        [FirestoreProperty]
        public string objName
        {
            get => obj.name ;
            set => obj.name  = value;
        }
    }

*/
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;


[FirestoreData]
public class SaveData
{

    private string userName = "ben52ben52";
    private int health =99;
    private float difficulty = 1.5f;

    [FirestoreProperty]
    public string UserName
    {
        get => userName;
        set => userName = value;
    }

    [FirestoreProperty]
    public int Health
    {
        get => health;
        set => health = value;
    }

    [FirestoreProperty]
    public float Difficulty
    {
        get => difficulty;
        set => difficulty = value;
    }



}


*/

