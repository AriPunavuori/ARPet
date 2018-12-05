using UnityEngine;

public class ResourceManager : Singelton<ResourceManager>
{
    public GameObject Plane { get; private set; }
    public GameObject World { get; private set; }
    public GameObject ARPalm { get; private set; }
    public GameObject TestSphere { get; private set; }

    private void Awake()
    {
        Plane = Resources.Load<GameObject>("Prefabs/Plane");
        World = Resources.Load<GameObject>("Prefabs/World");
        ARPalm = Resources.Load<GameObject>("Prefabs/ARPalm");
        TestSphere = Resources.Load<GameObject>("Prefabs/TestSphere");
    }
}
