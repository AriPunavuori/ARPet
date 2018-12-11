using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singelton<AudioManager>
{
    #region VARIABLES

    private AudioMixer audioMixer;
    private AudioMixerGroup[] audioMixerGroupOutputs;
    private AudioSource audioSource;

    public bool IsRecording { get; private set; }

    private readonly int recordLenghInSeconds = 10;

    #endregion VARIABLES

    #region PROPERTIES

    public string DeviceName { get; private set; }

    public float AudioDecibel
    {
        get
        {
            float currentDecibelValue;
            audioMixer.GetFloat("Microphone", out currentDecibelValue);
            return currentDecibelValue;            
        }
    }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioMixer = ResourceManager.Instance.AudioMixer;

        audioMixerGroupOutputs = audioMixer.FindMatchingGroups(string.Empty);
    }

    private void Start()
    {
        audioSource.outputAudioMixerGroup = audioMixerGroupOutputs[3];

        foreach (var device in Microphone.devices)
        {
            DeviceName = device.ToString();
            return;
        }

        StartRecording(DeviceName ?? "NULL", true, 10, AudioSettings.outputSampleRate);
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    public void StartRecording(string deviceName, bool isLooping, int recordLenghInSeconds, int frequency)
    {
        if (DeviceName == null || DeviceName == string.Empty)
            return;

        audioSource.clip = Microphone.Start(deviceName, isLooping, recordLenghInSeconds, frequency);
        audioSource.loop = true;

        if (IsMicrophoneRecording(deviceName))
        {
            while(!(Microphone.GetPosition(deviceName) > 0))
            {
                // We wait...
            }

            audioSource.Play();
        }
        else
        {
            Debug.LogError("Foo!?");
        }
    }

    public bool IsMicrophoneRecording(string deviceName)
    {
        IsRecording = Microphone.IsRecording(deviceName);
        return IsRecording;
    }

    public void StopRecording(string deviceName)
    {
        Microphone.End(deviceName);
    }

    #endregion CUSTOM_FUNCTIONS
}
