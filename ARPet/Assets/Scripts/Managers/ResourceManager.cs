using HuaweiARUnitySDK;
using UnityEngine;
using UnityEngine.Audio;

public class ResourceManager : Singelton<ResourceManager>
{
    public ARConfigBase PetARConfig { get; private set; }

    public GameObject PalmPrefab { get; private set; }
    public GameObject AnchorPrefab { get; private set; }
    public GameObject HorizontalPlanePrefab { get; private set; }
    public GameObject WorldObjectPrefab { get; private set; }
    public GameObject TouchHitPointPrefab { get; private set; }

    public GameObject PetPrefab { get; private set; }

    public Material ARBackground_mat { get; private set; }

    public AudioMixer AudioMixer { get; private set; }

    private void Awake()
    {
        PetARConfig = Resources.Load<ARConfigBase>("Prefabs/ArConfig/PetARConfig");

        PalmPrefab = Resources.Load<GameObject>("Prefabs/Models/Palm");
        AnchorPrefab = Resources.Load<GameObject>("Prefabs/Models/Anchor");
        HorizontalPlanePrefab = Resources.Load<GameObject>("Prefabs/Models/HorizontalPlane");
        WorldObjectPrefab = Resources.Load<GameObject>("Prefabs/Models/World");
        TouchHitPointPrefab = Resources.Load<GameObject>("Prefabs/Models/TouchHitPoint");
        PetPrefab = Resources.Load<GameObject>("Prefabs/Models/Pet");

        ARBackground_mat = Resources.Load<Material>("Prefabs/Materials/ARBackground_mat");

        AudioMixer = Resources.Load<AudioMixer>("Audio/Mixers/AudioMixer");
    }
}
