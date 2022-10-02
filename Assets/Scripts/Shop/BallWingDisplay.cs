using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BallWingDisplay : MonoBehaviour
{
    public Ball_Wing item;
    public Image preview, previewInHintPanel;
    public TextMeshProUGUI condition;
    [SerializeField] GameObject hintPanel;
    [SerializeField] GameObject disableImage,checkIcon;
    void OpenHintPanel()
    {
        hintPanel.SetActive(true);
        HintPanel.Instance.OpenHintPanel(item.Type);
        previewInHintPanel.sprite = item.Sprite;
        condition.text = item.Condition;

        preview.color = Color.white;
    }
    public void Init(Ball_Wing item)
    {
        this.item = item;
        preview.sprite = item.Sprite;
        if (PlayerPrefs.GetInt(item.Type + "IdSelected") == item.ID)
            checkIcon.SetActive(true);
        if (PlayerPrefManager.Instance.GetUnlockStatus(item.Type, item.ID) == 0)
        {
            disableImage.SetActive(true);
        }
        else
            disableImage.SetActive(false);
    }
    public void OnClick()
    {
        PlayerPrefManager.Instance.ResetTrialId();
        PlayerPrefs.SetInt("Trial"+item.Type+"Id", item.ID);

        if (PlayerPrefManager.Instance.GetUnlockStatus(item.Type, item.ID) == 1) //purchase
        {
            ShopController.Instance.DeactiveCheckIcon(item.Type,PlayerPrefs.GetInt(item.Type+"IdSelected"));
            PlayerPrefs.SetInt(item.Type + "IdSelected", item.ID);
            checkIcon.SetActive(true);
        }
        else OpenHintPanel();
    }
}
