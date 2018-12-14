using HuaweiARUnitySDK;
using UnityEngine;
using UnityEngine.Audio;

public class ResourceManager : Singelton<ResourceManager>
{
    public ARConfigBase HuabotARConfig { get; private set; }

    public GameObject PalmPrefab { get; private set; }
    public GameObject AnchorPrefab { get; private set; }
    public GameObject HorizontalPlanePrefab { get; private set; }
    public GameObject WorldObjectPrefab { get; private set; }
    public GameObject HitIndicatorPrefab { get; private set; }

    public GameObject BlockPrefab { get; private set; }

    public GameObject HuabotPrefab { get; private set; }

    public Material ARBackground_mat { get; private set; }

    public AudioMixer AudioMixer { get; private set; }

    public GameObject Foo;

    private void Awake()
    {

        HuabotARConfig = Resources.Load<ARConfigBase>("Prefabs/ArConfig/HuabotARConfig");

        PalmPrefab = Resources.Load<GameObject>("Prefabs/Models/Palm");
        AnchorPrefab = Resources.Load<GameObject>("Prefabs/Models/Anchor");
        HorizontalPlanePrefab = Resources.Load<GameObject>("Prefabs/Models/HorizontalPlane");
        WorldObjectPrefab = Resources.Load<GameObject>("Prefabs/Models/World");
        HitIndicatorPrefab = Resources.Load<GameObject>("Prefabs/Models/HitIndicator");
        HuabotPrefab = Resources.Load<GameObject>("Prefabs/Models/Huabot");

        BlockPrefab = Resources.Load<GameObject>("Prefabs/Models/ARBox");

        ARBackground_mat = Resources.Load<Material>("Prefabs/Materials/ARBackground_mat");

        AudioMixer = Resources.Load<AudioMixer>("Audio/Mixers/AudioMixer");
    }
}
