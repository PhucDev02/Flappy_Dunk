using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameController instance;
    //[SerializeField] GameObject  ceil, floor;
    [SerializeField] BallController ball;
    [SerializeField] SpriteRenderer[] listToFade;
    public int Score;
    public bool IsGameOver;
    public int SwishCount;
    void Awake()
    {
        Time.timeScale = 0;
        Score = 0;
        instance = this;
        IsGameOver = false;
        SwishCount = 0;
        FadeGameObject(0, 0.01f);
    }
    public void FadeGameObject(float endValue, float time)
    {
        ball.Fade(endValue, time);
        foreach (SpriteRenderer obj in listToFade)
        {
            obj.DOFade(endValue, time).SetEase(Ease.InCubic).SetUpdate(true).OnComplete(() =>
            {
                Time.timeScale = 0;
            });
        }
    }
    public void InitStatus()
    {
        resetScore();
        ball.reset();
        IsGameOver = false;
        SwishCount = 0;
        HoopsManager.Instance.NewGame();
        CameraScript.Instance.reset();
        BackgroundScript.Instance.reset();
        UI_Controller.instance.ResetSecondChance();

        foreach (SpriteRenderer obj in listToFade)
        {
            obj.DOKill();
            obj.DOFade(1, 0.2f).SetUpdate(true);
        }
    }
    public void IncreaseScore()
    {
        if (SwishCount == 0)
            Score += 1;
        else
        {
            UI_Controller.instance.UpdateSwish(SwishCount + 1);
            Score += (SwishCount + 1);
        }
        UI_Controller.instance.UpdateScore(Score);
    }
    public void IncreaseSwish()
    {
        this.PostEvent(EventID.OnSwish);
        SwishCount++;
        ActiveParticle();
        UI_Controller.instance.Vibrate();
        if (SwishCount == 1) AudioManager.Instance.Play("SwishX2");
        if (SwishCount == 2) AudioManager.Instance.Play("SwishX3");
        if (SwishCount >= 3) AudioManager.Instance.Play("SwishXN");
        if (SwishCount >= 2)
            CameraScript.Instance.Shake();
    }
    public void ActiveParticle()
    {
        ball.ActiveParticle(SwishCount);
    }
    public void DeactiveParticle()
    {
        ball.DeactiveParticle();
    }
    private void resetScore()
    {
        Score = 0;
        UI_Controller.instance.UpdateScore(Score);
    }
    public void ActiveSecondChance()
    {
        IsGameOver = false;
        SwishCount = 0;
        ball.reset();
        ball.transform.position = new Vector3(HoopsManager.Instance.getHorizontalLastHoop(), 0.315f, 0);
        ball.GetComponent<Rigidbody2D>().drag = 0;
        CameraScript.Instance.MoveToBall();
    }
}
