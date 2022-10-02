using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChallengeController : MonoBehaviour
{
    // Start is called before the first frame update
    public static ChallengeController Instance;
    //[SerializeField] GameObject  ceil, floor;
    [SerializeField] BallController ball;
    [SerializeField] SpriteRenderer[] listToFade;
    [SerializeField] GameObject levelTmp;
    public bool IsGameOver;
    public int SwishCount;
    void Awake()
    {
        Time.timeScale = 0;
        Instance = this;
        IsGameOver = false;
        SwishCount = 0;
        FadeGameObject(0, 0.01f);
    }
    public void LoadLevel()
    {
        levelTmp= Instantiate(GameManager.Instance.challenges[PlayerPrefs.GetInt("ChallengeIdSelected")-1].levelPrefab);
        CameraScript.Instance.AssignFollowObject();
        ball = GameObject.FindGameObjectWithTag("Player").GetComponent<BallController>();
        FadeGameObject(1, 0.01f);
        IsGameOver = false;
    }
    public void UnloadLevel()
    {
        Destroy(levelTmp);
    }
    public void FadeGameObject(float endValue, float time)
    {
        foreach (SpriteRenderer obj in listToFade)
        {
            obj.DOFade(endValue, time).SetEase(Ease.InCubic).SetUpdate(true).OnComplete(() =>
            {
                Time.timeScale = 0;
            });
        }
    }
    public void IncreaseSwish()
    {
        this.PostEvent(EventID.OnSwish);
        SwishCount++;
        ActiveParticle();
        UI_Challenge.Instance.UpdateSwish(SwishCount+1);
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
    public void ActiveSecondChance()
    {
        IsGameOver = false;
        SwishCount = 0;
        ball.reset();
        ball.transform.position = new Vector3(HoopsManagerChallenge.Instance.getHorizontalLastHoop(), 0.315f, 0);
        CameraScript.Instance.MoveToBall();
    }
}
