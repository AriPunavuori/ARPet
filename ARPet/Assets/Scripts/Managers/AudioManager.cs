using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singelton<AudioManager>
{
    #region VARIABLES

    #endregion VARIABLES

    #region PROPERTIES

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Start()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            UIManager.Instance.UpdateDeviceText = device[0].ToString();
        }
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    public void StartRecording()
    {
       
    }

    #endregion CUSTOM_FUNCTIONS
}
