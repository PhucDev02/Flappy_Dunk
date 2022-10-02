using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public class UI_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public static UI_Controller instance;
    [SerializeField] GameObject menuPanel, gamePlayPanel, gameOverPanel, secondChancePanel;
    [SerializeField] TextMeshProUGUI score, swishText;
    [SerializeField] Color darkRed, darkOrange;
    [SerializeField] CanvasGroup HUD;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        HoopsManager.Instance.FadeAllHoops(0, 0.01f);
        if (GameManager.mode == GameMode.Trial)
        {
            NewGame();
        }
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gamePlayPanel.SetActive(false);
        GameController.instance.IsGameOver = true;
        //UI_Menu.Instance.UpdateScore(GameController.instance.Score);
        AudioManager.Instance.Play("Wrong");
        this.PostEvent(EventID.OnDieEndlessGame);
        if (secondChancePanel.GetComponent<SecondChancePanel>().IsActived == false)
        {
            StartCoroutine(waitToOpenSecondChancePanel(2.0f));
        }
        else
            StartCoroutine(waitToOpenMenu(2.0f));
    }
    IEnumerator waitToOpenMenu(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OpenMenu();
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
    public void OpenMenu()
    {
        gameOverPanel.SetActive(false);
        secondChancePanel.transform.DOMoveX(-5, 0.01f);
        menuPanel.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true).OnComplete(() =>
        {
            GameManager.Instance.ChangeToEndlessMode();
        });
        UI_Menu.Instance.UpdateScore(GameController.instance.Score);
        HoopsManager.Instance.FadeAllHoops(0, 0.8f);
        GameController.instance.FadeGameObject(0, 0.8f);
    }
    public void NewGame()
    {
        secondChancePanel.SetActive(false);
        secondChancePanel.transform.DOKill();
        secondChancePanel.transform.DOMoveX(-5, 0f);
        menuPanel.transform.DOKill();
        menuPanel.transform.DOMoveX(-5, 0f).SetUpdate(true);
        gamePlayPanel.SetActive(true);

        HUD.DOFade(0, 0).SetUpdate(true);
        HUD.DOFade(1, 1).SetUpdate(true);

        gameOverPanel.SetActive(false);
        AudioManager.Instance.Play("Whistle");
        GameController.instance.InitStatus();
        HoopsManager.Instance.FadeAllHoops(1, 0.01f);
        this.PostEvent(EventID.OnNewEndlessGame);
    }
    public void UpdateScore(int point)
    {
        score.text = point.ToString();
        score.transform.DOScale(1.2f,0.1f).OnComplete(()=> { score.transform.DOScale(1.0f, 0.1f).SetUpdate(true); }).SetUpdate(true);
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
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
    public void ActiveSecondChance()
    {
        this.PostEvent(EventID.OnActiveSecondChance);
        GameController.instance.ActiveSecondChance();
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
            MMVibrationManager.Haptic(HapticTypes.LightImpact,true);
    }


}
