using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text StateText;

    private void Awake()
    {
        StateText = GameMaster.Instance.HUDCanvas.transform.Find("StateText").GetComponent<Text>();
    }

    public void ChangeStateText(string newText)
    {
        StateText.text = newText;
    }
}
