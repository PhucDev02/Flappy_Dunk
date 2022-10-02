using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    //Type + "IdSelected"
    //Type + index
    //Trial + type + Id
    //Challenge + ID //isComplete
    //ChallengeIdSelected

    //TotalEndlessPlayed
    //TotalSwish
    //TotalPassHoop
    //TotalSecondChance
    //TotalChallengeCompleted
    public static PlayerPrefManager Instance;
    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Ball0", 1);
        PlayerPrefs.SetInt("Ball1", 1);
        PlayerPrefs.SetInt("Wing0", 1);
        PlayerPrefs.SetInt("Hoop0", 1);
        PlayerPrefs.SetInt("Flame0", 1);
        GameManager.Instance.UpdateStatus();
    }
    public int GetCompleteChallengeStatus(int ID)
    {
        return PlayerPrefs.GetInt("Challenge" + ID);
    }
    public void SetCompleteChallengeStatus(int ID)
    {
        if (PlayerPrefs.GetInt("Challenge" + ID) == 0)
        {
            PlayerPrefs.SetInt("Challenge" + ID, 1);
            IncreaseInt("TotalChallengeCompleted", 1);
        }
    }
    public int GetUnlockStatus(string type, int ID)
    {
        return PlayerPrefs.GetInt(type + ID);
        //1 is purchased
    }
    public void SetUnlockStatus(string type, int ID)
    {
        //1 is purchased
        if (GetUnlockStatus(type, ID) == 0)
        {
            PlayerPrefs.SetInt(type + ID, 1);
            Logger.Log("Unlock" + type + ID);
        }
    }
    public void IncreaseInt(string name, int step)
    {
        PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name) + step);
        Logger.Log(name + PlayerPrefs.GetInt(name));
    }
    public void ResetTrialId()
    {
        PlayerPrefs.SetInt("TrialHoopId", PlayerPrefs.GetInt("HoopIdSelected"));
        PlayerPrefs.SetInt("TrialBallId", PlayerPrefs.GetInt("BallIdSelected"));
        PlayerPrefs.SetInt("TrialWingId", PlayerPrefs.GetInt("WingIdSelected"));
        PlayerPrefs.SetInt("TrialFlameId", PlayerPrefs.GetInt("FlameIdSelected"));

    }
}
