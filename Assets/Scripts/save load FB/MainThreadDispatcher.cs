using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    private static MainThreadDispatcher _instance;

    public static MainThreadDispatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("MainThreadDispatcher");
                _instance = obj.AddComponent<MainThreadDispatcher>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public void Enqueue(Action action)
    {
        StartCoroutine(ActionWrapper(action));
    }

    private IEnumerator ActionWrapper(Action action)
    {
        action();
        yield return null;
    }
}

