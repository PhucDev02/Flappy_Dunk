using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UI_Challenge : MonoBehaviour
{
    // Start is called before the first frame update
    public static UI_Challenge Instance;
    [SerializeField] GameObject challengePanel, pauseButton, gameOverPanel, secondChancePanel, hintPanel;
    [SerializeField] TextMeshProUGUI swishText, completeText,startText;
    [SerializeField] CanvasGroup HUD;
    void Awake()
    {
        Instance = this;
    }
    public void CompleteLevel()
    {
        startText.text = "Start";
        completeText.transform.DOMoveY(1.5f, 0.001f);
        completeText.DOFade(1, 0.001f);
        completeText.transform.DOMoveY(2.0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            completeText.DOFade(0, 3.0f);
        }
        );
        ChallengeController.Instance.IsGameOver = true;
        ChallengeController.Instance.SwishCount = 0;
        hintPanel.SetActive(false);
        pauseButton.SetActive(false);
        PlayerPrefManager.Instance.SetCompleteChallengeStatus(PlayerPrefs.GetInt("ChallengeIdSelected"));
        ChallengeManager.Instance.UpdateStatus();
        StartCoroutine(waitToOpenChallenge(3f));
    }
    public void GameOver()
    {
        startText.text = "Retry";
        gameOverPanel.SetActive(true);
        pauseButton.SetActive(false);
        ChallengeController.Instance.IsGameOver = true;
        AudioManager.Instance.Play("Wrong");
        if (secondChancePanel.GetComponent<SecondChancePanel>().IsActived == false)
        {
            StartCoroutine(waitToOpenSecondChancePanel(2.0f));
        }
        else
            StartCoroutine(waitToOpenChallenge(2.0f));
    }
    IEnumerator waitToOpenChallenge(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OpenChallenge();
    }
    IEnumerator waitToOpenSecondChancePanel(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OpenSecondChancePanel();
    }
    void OpenSecondChancePanel()
    {
        secondChancePanel.SetActive(true);
        secondChancePanel.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutCubic).SetUpdate(true);
    }
    public void OpenChallenge()
    {
        gameOverPanel.SetActive(false);
        secondChancePanel.transform.DOMoveX(-5, 0.01f);
        challengePanel.GetComponent<RectTransform>().DOLocalMoveX(0, 1f).SetEase(Ease.OutExpo).SetUpdate(true);
        pauseButton.SetActive(false);
        ChallengeController.Instance.FadeGameObject(0, 1.0f);
        ChallengeController.Instance.UnloadLevel();
        //HoopsManagerChallenge.Instance.FadeAllHoop(0, 1.0f);
    }
    public void ActiveChallengeMode()
    {
        secondChancePanel.SetActive(false);
        secondChancePanel.transform.DOMoveX(-5, 0.01f);
        challengePanel.transform.DOMoveX(-5, 0.01f).SetUpdate(true);
        pauseButton.SetActive(true);
        gameOverPanel.SetActive(false);
        AudioManager.Instance.Play("Whistle");
        ChallengeController.Instance.LoadLevel();
        HoopsManagerChallenge.Instance.FadeAllHoop(1, 0.01f);
        secondChancePanel.GetComponent<SecondChancePanel>().reset();
    }
    public void UpdateSwish(int count)
    {
        swishText.text = "SWISH!\nX" + count.ToString();
        swishText.DOKill();
        swishText.transform.DOMoveY(1.5f, 0.001f);
        swishText.DOFade(1f, 0.001f);
        if (count <= 2)
            swishText.DOColor(Color.green, 0.001f);
        else if (count == 3)
            swishText.DOColor(SpriteHolder.Instance.darkOrange, 0.001f);
        else swishText.DOColor(SpriteHolder.Instance.darkRed, 0.001f);
        // 0.7 1.5
        swishText.transform.DOMoveY(2.0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            swishText.DOFade(0, 0.5f).SetEase(Ease.InCirc);
        }
        );
    }
    public void ActiveSecondChance()
    {
        this.PostEvent(EventID.OnActiveSecondChance);
        ChallengeController.Instance.ActiveSecondChance();
        secondChancePanel.GetComponent<SecondChancePanel>().IsActived = true;
        secondChancePanel.SetActive(false);
    }
    public void ResetSecondChance()
    {
        secondChancePanel.GetComponent<SecondChancePanel>().reset();
    }
    public void Vibrate()
    {
        if (PlayerPrefs.GetInt("AllowVibration") == 0)
            Handheld.Vibrate();
    }
    public void ChangeStartText()
    {
        startText.text = "Start";
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
}
