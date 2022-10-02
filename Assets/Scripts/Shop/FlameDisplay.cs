using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FlameDisplay : MonoBehaviour
{
    [SerializeField] public Flame item;
    [SerializeField] public Image preview, previewInHintPanel;
    [SerializeField] public TextMeshProUGUI condition;
    [SerializeField] GameObject hintPanel;
    [SerializeField] GameObject disableImage, checkIcon;
    public void OpenHintPanel()
    {
        hintPanel.SetActive(true);
        HintPanel.Instance.OpenHintPanel("Flame");
        previewInHintPanel.sprite = SpriteHolder.Instance.flame;
        previewInHintPanel.color = item.Color;

        condition.text = item.Condition;
    }
    public void Init(Flame item)
    {
        this.item = item;
        preview.sprite = SpriteHolder.Instance.flame;
        preview.color = item.Color;
        if (PlayerPrefs.GetInt("FlameIdSelected") == item.ID)
            checkIcon.SetActive(true);
        if (PlayerPrefManager.Instance.GetUnlockStatus("Flame", item.ID) == 0)
        {
            disableImage.SetActive(true);
        }
        else
            disableImage.SetActive(false);
    }
    public void OnClick()
    {
        PlayerPrefManager.Instance.ResetTrialId();
        PlayerPrefs.SetInt("TrialFlameId", item.ID);

        if (PlayerPrefManager.Instance.GetUnlockStatus("Flame", item.ID) == 1) //purchase
        {
            ShopController.Instance.DeactiveCheckIcon("Flame", PlayerPrefs.GetInt("FlameIdSelected"));
            PlayerPrefs.SetInt("FlameIdSelected", item.ID);
            checkIcon.SetActive(true);
        }
        else OpenHintPanel();
    }
}
