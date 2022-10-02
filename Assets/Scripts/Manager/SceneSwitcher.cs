using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
   public void LoadMenuScene()
    {
        SceneManager.LoadScene("Game");
        GameManager.mode = GameMode.Endless;
    }
    public void LoadShopScene()
    {
        SceneManager.LoadScene("Shop");
    }
    public void LoadTrialMode()
    {
        SceneManager.LoadScene("Game");
        GameManager.mode = GameMode.Trial;
    }
    public void LoadChallengesMode()
    {
        SceneManager.LoadScene("Challenge");
        GameManager.mode = GameMode.Challenge;
    }
    public void ClickSound()
    {
        AudioManager.Instance.Play("Click");
    }
}
