using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AchievementManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform parent;
    [SerializeField] GameObject newSkinPopup;
    GameObject tmp;
    int totalSwishInAGame = 0, comboSwish = 0;
    void Start()
    {
        this.RegisterListener(EventID.OnNewEndlessGame, (param) => OnNewEndlessGame());
        this.RegisterListener(EventID.OnDieEndlessGame, (param) => OnDieEndlessGame());
        this.RegisterListener(EventID.OnPassHoop, (param) => OnPassHoop());
        this.RegisterListener(EventID.OnSwish, (param) => OnSwish());
        this.RegisterListener(EventID.OnActiveSecondChance, (param) => OnActiveSecondChance());
        this.RegisterListener(EventID.OnUnlockSkin, (param) => OnUnlockSkin());
        this.RegisterListener(EventID.OnCompleteChallenge, (param) => OnCompleteChallenge());
        this.RegisterListener(EventID.OnCollideWithHoop, (param) => OnCollideWithHoop());
    }


    private void OnCompleteChallenge()
    {
        Logger.Log("Challenge complete1: " + PlayerPrefs.GetInt("TotalChallengeCompleted").ToString());
        if (PlayerPrefs.GetInt("TotalChallengeCompleted") == 1)
        {
            unlockSkin("Hoop", 3);
        }
        if (PlayerPrefs.GetInt("TotalChallengeCompleted") == 2)
        {
            unlockSkin("Hoop", 4);
        }
        if (PlayerPrefs.GetInt("TotalChallengeCompleted") == 3)
        {
            unlockSkin("Hoop", 5);
        }
    }

    private void OnUnlockSkin()
    {
        UI_Menu.Instance.UpdateStatus();
        if (GameManager.Instance.AmountItemUnlocked >= 10)
        {
            unlockSkin("Ball", 5);
        }
    }
    void unlockSkin(string typeName, int ID)
    {
        if (PlayerPrefManager.Instance.GetUnlockStatus(typeName, ID) == 0)
        {
            PlayerPrefManager.Instance.SetUnlockStatus(typeName, ID);
            GameManager.Instance.UpdateStatus();
            tmp = Instantiate(newSkinPopup, parent);
            tmp.GetComponent<NewSkinPopup>().Init(typeName, ID);
            Logger.Log("instantiate");
            this.PostEvent(EventID.OnUnlockSkin);
        }
    }
    private void OnCollideWithHoop()
    {
        comboSwish = 0;
    }
    private void OnSwish()
    {
        totalSwishInAGame++;
        comboSwish++;
        PlayerPrefManager.Instance.IncreaseInt("TotalSwish", 1);
        if (PlayerPrefs.GetInt("TotalSwish") >= 20)
        {
            unlockSkin("Ball", 4);
        }
        if (comboSwish >= 5)
            unlockSkin("Flame", 3);
    }

    private void OnPassHoop()
    {
        PlayerPrefManager.Instance.IncreaseInt("TotalPassHoop", 1);
        if (PlayerPrefs.GetInt("TotalPassHoop") >= 5)
        {
            unlockSkin("Wing", 1);
        }
        if (PlayerPrefs.GetInt("TotalPassHoop") >= 10)
        {
            unlockSkin("Wing", 2);
        }
        if (PlayerPrefs.GetInt("TotalPassHoop") >= 20)
        {
            unlockSkin("Wing", 3);
        }
        if (PlayerPrefs.GetInt("TotalPassHoop") >= 30)
        {
            unlockSkin("Ball", 6);
        }
        if (PlayerPrefs.GetInt("TotalPassHoop") >= 40)
        {
            unlockSkin("Ball", 7);
        }
        if (PlayerPrefs.GetInt("TotalPassHoop") >= 50)
        {
            unlockSkin("Wing", 4);
        }

    }

    private void OnActiveSecondChance()
    {
        PlayerPrefManager.Instance.IncreaseInt("TotalSecondChance", 1);
        if (PlayerPrefs.GetInt("TotalSecondChance") >= 3)
        {
            unlockSkin("Hoop", 1);
        }
        if (PlayerPrefs.GetInt("TotalSecondChance") >= 6)
        {
            unlockSkin("Hoop", 2);
        }
    }

    private void OnDieEndlessGame()
    {
        if (GameController.instance.Score > 30)
            unlockSkin("Ball", 3);
        if (GameController.instance.Score == 29)
            unlockSkin("Ball", 2);
        Logger.Log(totalSwishInAGame.ToString());
        if (totalSwishInAGame >= 10)
        {
            unlockSkin("Flame", 4);
        }
        totalSwishInAGame = 0;
    }

    void OnNewEndlessGame()
    {
        PlayerPrefManager.Instance.IncreaseInt("TotalEndlessPlayed", 1);
        if (PlayerPrefs.GetInt("TotalEndlessPlayed") >= 5)
        {
            unlockSkin("Flame", 1);
        }
        if (PlayerPrefs.GetInt("TotalEndlessPlayed") >= 10)
        {
            unlockSkin("Flame", 2);
        }
    }
}
