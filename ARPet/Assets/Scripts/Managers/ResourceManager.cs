using UnityEngine;

public class ResourceManager : Singelton<ResourceManager>
{
    public GameObject WorldPrefab { get; private set; }
    //public GameObject PlanePrefab { get; private set; }

    private void Awake()
    {
        WorldPrefab = Resources.Load<GameObject>("Prefabs/World");
        //PlanePrefab = Resources.Load<GameObject>("Prefabs/Plane");
    }
}
