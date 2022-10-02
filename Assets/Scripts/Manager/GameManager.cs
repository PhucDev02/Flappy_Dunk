using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Challenge, Endless, Trial
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Ball_Wing[] balls;
    public Ball_Wing[] wings;
    public Hoop[] hoops;
    public Flame[] flames;
    public Challenge[] challenges;
    public int AmountItem, AmountItemUnlocked,AmountChallengeComplete;
    public static GameMode mode;
    private void Awake()
    {
        Application.targetFrameRate = 120;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].ID = i;
            balls[i].Type = "Ball";
        }
        for (int i = 0; i < wings.Length; i++)
        {
            wings[i].ID = i;
            wings[i].Type = "Wing";
        }
        for (int i = 0; i < hoops.Length; i++)
            hoops[i].ID = i;
        for (int i = 0; i < flames.Length; i++)
            flames[i].ID = i;
        if (mode != GameMode.Trial)
            mode = GameMode.Endless;
        AmountItem = balls.Length + wings.Length + hoops.Length + flames.Length;
        
    }
    public void CountChallengeCompleted()
    {
        AmountChallengeComplete = 0;
        for(int i=0;i<challenges.Length;i++)
        {
            AmountChallengeComplete += PlayerPrefManager.Instance.GetCompleteChallengeStatus(i + 1);
        }
    }
    public void CountItemUnlocked()
    {
        AmountItemUnlocked = 0;
        for (int i = 0; i < balls.Length; i++)
            AmountItemUnlocked += PlayerPrefs.GetInt("Ball" + balls[i].ID);
        for (int i = 0; i < wings.Length; i++)
            AmountItemUnlocked += PlayerPrefs.GetInt("Wing" + wings[i].ID);
        for (int i = 0; i < hoops.Length; i++)
            AmountItemUnlocked += PlayerPrefs.GetInt("Hoop" + hoops[i].ID);
        for (int i = 0; i < flames.Length; i++)
            AmountItemUnlocked += PlayerPrefs.GetInt("Flame" + flames[i].ID);
    }
    public void UpdateStatus()
    {
        CountItemUnlocked();
        CountChallengeCompleted();
    }
    public void ChangeToEndlessMode()
    {
        if(mode == GameMode.Trial)
            mode = GameMode.Endless;
        PlayerPrefManager.Instance.ResetTrialId();
    }
    public void gameOver()
    {
        if (mode == GameMode.Challenge)
            UI_Challenge.Instance.GameOver();
        else
            UI_Controller.instance.GameOver();
    }
}
