using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UI_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public static UI_Menu Instance;
    [SerializeField] TextMeshProUGUI bestScore, lastScore;
    [SerializeField] Image challangeFill, skinFill;
    [SerializeField] Image previewFrontWing, previewBackWing, previewBall;
    [SerializeField] Image flare;
    [SerializeField] TextMeshProUGUI newBestScore;
    [SerializeField] ParticleSystem[] effect;
    private void Awake()
    {
        Instance = this;
        bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        lastScore.text = "LAST: " + PlayerPrefs.GetInt("LastScore").ToString();

    }
    private void Start()
    {
        UpdateStatus();
        previewBackWing.sprite = GameManager.Instance.wings[PlayerPrefs.GetInt("WingIdSelected")].Sprite;
        previewFrontWing.sprite = previewBackWing.sprite;
        previewBall.sprite = GameManager.Instance.balls[PlayerPrefs.GetInt("BallIdSelected")].Sprite;
    }
    public void UpdateStatus()
    {

        GameManager.Instance.UpdateStatus();

        skinFill.fillAmount = (float)GameManager.Instance.AmountItemUnlocked / GameManager.Instance.AmountItem;
        skinFill.color = SpriteHolder.Instance.colorByPercent(skinFill.fillAmount);

        challangeFill.fillAmount = (float)GameManager.Instance.AmountChallengeComplete / GameManager.Instance.challenges.Length;
        challangeFill.color = SpriteHolder.Instance.colorByPercent(challangeFill.fillAmount);

    }
    public void UpdateScore(int score)
    {
        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
            NewBestScoreEffect();
            // effect
        }
        bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        PlayerPrefs.SetInt("LastScore", score);
        lastScore.text = "LAST: " + PlayerPrefs.GetInt("LastScore").ToString();
    }
    public void NewBestScoreEffect()
    {
        foreach (ParticleSystem p in effect)
            p.Play();
        //flare.DOFade(0.6f, 0.6f).OnComplete(() => { flare.DOFade(1, 0.6f); }).SetLoops(6).OnComplete(() => { flare.DOFade(0, 0.6f); });

        //newBestScore.transform.DOScale(0, 0.01f).OnComplete(() => { newBestScore.transform.DOScale(1, 0.5f); }).OnComplete(() =>
        //{
        //    newBestScore.transform.DOScale(0.6f, 0.6f).OnComplete(() =>
        //    {
        //        newBestScore.transform.DOScale(1, 0.6f);
        //    }
        //    ).SetLoops(6).OnComplete(() =>
        //    {
        //        newBestScore.DOFade(0, 0.6f);
        //    }
        //    );
        //});
        AudioManager.Instance.Play("Cheer");
    }
}
