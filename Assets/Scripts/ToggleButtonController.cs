using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image soundBtn, vibrationBtn;
    private void Start()
    {
        updateButton();
    }
    public void SwitchVibration()
    {
        PlayerPrefs.SetInt("AllowVibration", 1 - PlayerPrefs.GetInt("AllowVibration"));
        UI_Controller.instance.Vibrate();
    }
    public void SwitchSound()
    {
        PlayerPrefs.SetInt("AllowSound", 1 - PlayerPrefs.GetInt("AllowSound"));
    }
    private void Update()
    {
        updateButton();
    }
    void updateButton()
    {
        if (PlayerPrefs.GetInt("AllowSound") == 0)
            soundBtn.sprite = SpriteHolder.Instance.soundOn;
        else
            soundBtn.sprite = SpriteHolder.Instance.soundOff;
        if (PlayerPrefs.GetInt("AllowVibration") == 0)
            vibrationBtn.sprite = SpriteHolder.Instance.vibrationOn;
        else
            vibrationBtn.sprite = SpriteHolder.Instance.vibrationOff;
    }
}
