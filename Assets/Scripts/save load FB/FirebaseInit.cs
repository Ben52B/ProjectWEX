using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseInit : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase is ready to use.");
                // Firebase is ready to use.
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
                // Firebase is not ready. Handle the error appropriately.
            }
        });
    }
}
