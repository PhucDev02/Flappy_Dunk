using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HoopDisplay : MonoBehaviour
{
    [SerializeField]public Hoop item;
    [SerializeField]public Image previewFrontImg,previewBackImg, previewBackInHintPanel,previewFrontInHintPanel;
    [SerializeField]public TextMeshProUGUI condition;
    [SerializeField] GameObject hintPanel;
    [SerializeField] GameObject disableImage, checkIcon;

    void OpenHintPanel()
    {
        hintPanel.SetActive(true);
        HintPanel.Instance.OpenHintPanel("Hoop");
        previewBackInHintPanel.sprite = item.BackSprite;
        previewFrontInHintPanel.sprite = item.FrontSprite;
        condition.text = item.Condition;
    }
    public void Init(Hoop item)
    {
        this.item = item;
        previewBackImg.sprite = item.BackSprite;
        previewFrontImg.sprite = item.FrontSprite;
        if (PlayerPrefs.GetInt("HoopIdSelected") == item.ID)
            checkIcon.SetActive(true);
        if (PlayerPrefManager.Instance.GetUnlockStatus("Hoop", item.ID) == 0)
        {
            disableImage.SetActive(true);
        }
        else
            disableImage.SetActive(false);
    }
    public void OnClick()
    {
        PlayerPrefManager.Instance.ResetTrialId();
        PlayerPrefs.SetInt("TrialHoopId", item.ID);
        if (PlayerPrefManager.Instance.GetUnlockStatus("Hoop", item.ID) == 1) //purchase
        {
            ShopController.Instance.DeactiveCheckIcon("Hoop", PlayerPrefs.GetInt("HoopIdSelected"));
            PlayerPrefs.SetInt("HoopIdSelected", item.ID);
            checkIcon.SetActive(true);
        }
        else OpenHintPanel();
    }
}
