using UnityEngine;
using System.Collections.Generic;
using HuaweiARUnitySDK;

public class ResourceManager : Singelton<ResourceManager>
{
    public T GetFromResources<T>(string folderName, string objectName) where T : Object
    {
        var resource = Resources.Load<T>("Prefabs/" + folderName + "/" + objectName) as T;

        if (resource != null)
        {
            return resource;
        }
        else
        {
            Debug.LogError(objectName + " is not in: " + folderName + " folder");
            return null;
        }
    }
}
