using UnityEngine;

public class GameMaster : Singelton<GameMaster>
{
    public GameObject HUDCanvas { get; private set; }
    public GameObject Managers { get; private set; }
    public GameObject Others { get; private set; }

    private void Awake()
    {
        HUDCanvas = transform.Find("HUDCanvas").gameObject;
        Managers = transform.Find("Managers").gameObject;
        Others = transform.Find("Others").gameObject;
    }
}
