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
<<<<<<< HEAD
    public GameObject HitIndicatorPrefab { get; private set; }
=======
    public GameObject HitPointIndicatorPrefab { get; private set; }
>>>>>>> master
    public GameObject BlockPrefab { get; private set; }

    public GameObject PetPrefab { get; private set; }

    public Material ARBackground_mat { get; private set; }

    public AudioMixer AudioMixer { get; private set; }

    private void Awake()
    {
        HuabotARConfig = Resources.Load<ARConfigBase>("Prefabs/ArConfig/HuabotARConfig");

        PalmPrefab = Resources.Load<GameObject>("Prefabs/Models/Palm");
        AnchorPrefab = Resources.Load<GameObject>("Prefabs/Models/Anchor");
        HorizontalPlanePrefab = Resources.Load<GameObject>("Prefabs/Models/HorizontalPlane");
        WorldObjectPrefab = Resources.Load<GameObject>("Prefabs/Models/World");
<<<<<<< HEAD
        HitIndicatorPrefab = Resources.Load<GameObject>("Prefabs/Models/HitIndicator");
=======
        HitPointIndicatorPrefab = Resources.Load<GameObject>("Prefabs/Models/HitPointIndicator");
>>>>>>> master
        PetPrefab = Resources.Load<GameObject>("Prefabs/Models/Pet");

        BlockPrefab = Resources.Load<GameObject>("Prefabs/Models/Block");

        ARBackground_mat = Resources.Load<Material>("Prefabs/Materials/ARBackground_mat");

        AudioMixer = Resources.Load<AudioMixer>("Audio/Mixers/AudioMixer");
    }
}
