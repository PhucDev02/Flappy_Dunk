using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChallengeDisplay : MonoBehaviour
{
    public Challenge challenge;
    public TextMeshProUGUI condition,headerHintPanel,number;
    public Image fill;
    private void Start()
    {
        UpdateStatus();
    }
    public void OnClick()
    {
        condition.text = challenge.Condition;
        headerHintPanel.text = "CHALLENGE " + challenge.ID;
        PlayerPrefs.SetInt("ChallengeIdSelected",challenge.ID);
    }
    public void UpdateStatus()
    {
        number.text = challenge.ID.ToString();
        if (PlayerPrefManager.Instance.GetCompleteChallengeStatus(challenge.ID) == 1)
        {
            fill.color = SpriteHolder.Instance.green;
            number.color = Color.white;
        }
        else
        {
            number.color = SpriteHolder.Instance.green;
            fill.color = Color.white;
        }
    }
}
