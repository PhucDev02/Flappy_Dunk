using UnityEngine;
using DG.Tweening;
public class HoopController : MonoBehaviour
{
    [SerializeField] GameObject body, axis;
    [SerializeField] Collider2D frontHoopCollider, backHoopCollider, sensor;
    // Start is called before the first frame update
    [SerializeField] bool isActive;
    [SerializeField] ParticleSystem starsEffect, smokeEffect, blastEffect, bigSmokeEffect;
    [SerializeField] SpriteRenderer frontHoopSR, backHoopSR, axisSR;
    Vector3 moveSpeed, initScale;
    [SerializeField] bool movable;
    public bool IsSwish;
    private void Awake()
    {
        moveSpeed = new Vector3(0, 1, 0);
    }
    void Start()
    {
        initScale = body.transform.localScale;
        isActive = true;
        if (GameManager.mode != GameMode.Trial)
            setSprite("HoopIdSelected");
        else setSprite("TrialHoopId");
    }
    private void OnEnable()
    {
        IsSwish = true;
        isActive = true;

        body.transform.localPosition = Vector3.zero;

        if (GameManager.mode != GameMode.Challenge)
        {
            transform.position = new Vector3(HoopsManager.Instance.getHorizontalPosition(), HoopsManager.Instance.RandomHeight());
            DeactiveColor();
            difficultyAdjustment();
        }
    }
    private void setSprite(string name)
    {
        frontHoopSR.sprite = GameManager.Instance.hoops[PlayerPrefs.GetInt(name)].FrontSprite;
        backHoopSR.sprite = GameManager.Instance.hoops[PlayerPrefs.GetInt(name)].BackSprite;
        starsEffect.textureSheetAnimation.SetSprite(0, GameManager.Instance.hoops[PlayerPrefs.GetInt(name)].startEffect);
    }
    private void Update()
    {
        if (movable == true)
            moveOnAxis();
    }
    void difficultyAdjustment()
    {
        if (GameController.instance.Score < 20)
        {
            body.transform.localScale = HoopsManager.Instance.scales[1];
            transform.localRotation = HoopsManager.Instance.angles[0];
            movable = false;
        }
        else if (GameController.instance.Score <= 60)
        {
            movable = (Random.Range(0, 100) > 70 ? true : false);
            if (movable == false)
            {
                transform.localRotation = HoopsManager.Instance.angles[Random.Range(0, 2)];
            }
            else
            {
                transform.localRotation = HoopsManager.Instance.angles[Random.Range(0, 100) > 30 ? 1 : 0];
            }
            body.transform.localScale = HoopsManager.Instance.scales[Random.Range(0, 100) > 30 ? 1 : 0];
        }
        else
        {
            movable = (Random.Range(0, 100) > 30) ? true : false;
            body.transform.localScale = HoopsManager.Instance.scales[Random.Range(0, 100) > 40 ? 0 : 1];
            transform.localRotation = HoopsManager.Instance.angles[Random.Range(0, 4)];
        }
        if (movable == true)
        {
            axis.SetActive(true);
        }
        else axis.SetActive(false);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position.x < Camera.main.transform.position.x - 2.8f)
        {
            if (GetIsGameOver() == false)
                if (isActive == true && CameraScript.Instance.isMovingToBall == false)
                    GameManager.Instance.gameOver();
        }
    }
    public void BallPass()
    {
        isActive = false;
        body.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        }
        );
        Fade(0, 1);
        backHoopCollider.enabled = false;
        frontHoopCollider.enabled = false;
        sensor.enabled = false;
        //
        movable = false;
        AudioManager.Instance.Play("PassHoop");
        this.PostEvent(EventID.OnPassHoop);
        spawnNewHoop();
    }
    private void spawnNewHoop()
    {
        if (GameManager.mode != GameMode.Challenge)
            HoopsManager.Instance.GetHoop().SetActive(true);
    }
    public void ActiveColor()
    {
        Fade(1, 0.001f);

        frontHoopCollider.enabled = true;
        backHoopCollider.enabled = true;
        sensor.enabled = true;
    }
    public void DeactiveColor()
    {
        Fade(0.5f, 0.001f);

        frontHoopCollider.enabled = false;
        backHoopCollider.enabled = false;
        sensor.enabled = false;
    }
    public void ActiveEffect(int swish)
    {
        if (swish == 1)
        {
            starsEffect.Play();
            smokeEffect.Play();
        }
        else if (swish >= 2)
        {
            starsEffect.Play();
            blastEffect.Play();
            bigSmokeEffect.Play();
        }
    }
    void moveOnAxis()
    {
        if (Mathf.Abs(body.transform.localPosition.y - 0.5f) <= 0.05f)
        {
            body.transform.localPosition = new Vector3(0, 0.5f - 0.05f, 0);
            moveSpeed *= -1;
        }
        if (Mathf.Abs(body.transform.localPosition.y + 1.0f) <= 0.05f)
        {
            body.transform.localPosition = new Vector3(0, -1f + 0.05f, 0);
            moveSpeed *= -1;
        }
        body.transform.localPosition += moveSpeed * Time.deltaTime;
    }
    public void Fade(float endValue, float time)
    {
        axisSR.DOKill();
        frontHoopSR.DOKill();
        backHoopSR.DOKill();
        frontHoopSR.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
        backHoopSR.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
        axisSR.DOFade(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true); ;
    }
    public void ToColor(Color endValue, float time)
    {
        axisSR.DOKill();
        frontHoopSR.DOKill();
        backHoopSR.DOKill();
        frontHoopSR.DOColor(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
        backHoopSR.DOColor(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
        axisSR.DOColor(endValue, time).SetEase(Ease.OutCubic).SetUpdate(true);
    }
    public bool GetIsGameOver()
    {
        if (GameManager.mode == GameMode.Challenge)
        {
            return ChallengeController.Instance.IsGameOver;
        }
        else return GameController.instance.IsGameOver;
    }

}
