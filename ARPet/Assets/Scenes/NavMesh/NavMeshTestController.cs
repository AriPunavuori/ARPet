using UnityEngine;

public class NavMeshTestController : MonoBehaviour
{
    public GameObject WorldTestPrefab;

    public GameObject TestPlayerPrefab;

    private TestPlayer Player;

    private void Awake()
    {
        var World = Instantiate(WorldTestPrefab).GetComponent<WorldTest>();
        World.Init(this);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Player.SetDestination(hit.point);
            }
        }
    }

    public void CreatePlayer()
    {
        Player = Instantiate(TestPlayerPrefab).GetComponent<TestPlayer>();
    }
}
