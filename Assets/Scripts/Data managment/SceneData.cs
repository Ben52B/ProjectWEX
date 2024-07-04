using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SceneData
{
    public List<ObjectData> objects = new List<ObjectData>();
}

[System.Serializable]
public class ObjectData
{   
    public string prefabName;

    /*
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public AnimationData animationData;
    public string photoPath; // Path to the uploaded photo
    public string videoPath; // Path to the uploaded video
}

[System.Serializable]
public class AnimationData
{
    public string animationClipName;
    public float playbackTime;
    public bool isPlaying;
    */
}
