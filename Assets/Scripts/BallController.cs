using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject frontWing, backWing, darkMode;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer darkBall, darkWing1, darkWing2;
    [SerializeField] Vector3 initFrontWingTransform, initBackWingTransform;
    [SerializeField] ParticleSystem smoke, flame;
    [SerializeField] float yForce, xForce;
    [SerializeField] Vector2 velocity;
    bool isBroken;

    private void Awake()
    {
        initBackWingTransform = new Vector3(0.65f, 0.6f, 0);
        initFrontWingTransform = new Vector3(-0.95f, 0.6f, 0);
        //xForce = 80;
        //yForce = 250;
        isBroken = false;
    }
    private void Start()
    {
        setSprite();
    }
    private void setSprite()
    {
        if (GameManager.mode != GameMode.Trial)
        {
            frontWing.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.wings[PlayerPrefs.GetInt("WingIdSelected")].Sprite;
            backWing.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.wings[PlayerPrefs.GetInt("WingIdSelected")].Sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.balls[PlayerPrefs.GetInt("BallIdSelected")].Sprite;
            flame.startColor = GameManager.Instance.flames[PlayerPrefs.GetInt("FlameIdSelected")].Color;

        }
        else
        {
            frontWing.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.wings[PlayerPrefs.GetInt("TrialWingId")].Sprite;
            backWing.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.wings[PlayerPrefs.GetInt("TrialWingId")].Sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.balls[PlayerPrefs.GetInt("TrialBallId")].Sprite;
            flame.startColor = GameManager.Instance.flames[PlayerPrefs.GetInt("TrialFlameId")].Color;
        }

        darkBall.color = flame.startColor;
        darkWing1.color = flame.startColor;
        darkWing2.color = flame.startColor;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI() && !GetIsGameOver()&&CameraScript.Instance.isMovingToBall==false)
        {
            Time.timeScale = 1;
            Jump();
        }
    }
    public void Jump()
    {
        rigidBody.velocity = velocity ;
        //rigidBody.AddForce(new Vector3(xForce, yForce), ForceMode2D.Force);
        animator.Play("Fly", 0, 0);
        AudioManager.Instance.Play("Flap");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ceil"))
        {
            DeactiveParticle();
            if (GetIsGameOver() == false)
            {
                wingFall("Ceil");
                GameManager.Instance.gameOver();
            }
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            DeactiveParticle();
            if (collision.relativeVelocity.magnitude > 2f)
                AudioManager.Instance.Play("Bounce");
            wingFall("Floor");
            rigidBody.drag = 0.4f;
            if (GetIsGameOver() == false)
            {
                rigidBody.angularDrag = 0;
                GameManager.Instance.gameOver();
            }
        }
    }
    private void wingFall(string tag)
    {
        frontWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        backWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        if (tag == "Ceil")
        {
            AudioManager.Instance.Play("Crash");
            frontWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, -10), 0), ForceMode2D.Force);
            backWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(10, 80), 0), ForceMode2D.Force);
        }
        if (tag == "Floor" && isBroken == false)
        {
            AudioManager.Instance.Play("Crash");
            isBroken = true;
            frontWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, -50), Random.Range(400, 500)), ForceMode2D.Force);
            backWing.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(50, 80), Random.Range(400, 500)), ForceMode2D.Force);
        }
    }
    public void reset()
    {
        setSprite();
        //ball
        Time.timeScale = 0;
        transform.position = new Vector3(-1.5f, 0.315f, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.angularDrag = 1;
        rigidBody.angularVelocity = 0;
        //wings
        isBroken = false;

        frontWing.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        backWing.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        frontWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        backWing.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        frontWing.transform.localPosition = initFrontWingTransform;
        backWing.transform.localPosition = initBackWingTransform;
        frontWing.transform.rotation = Quaternion.identity;
        backWing.transform.rotation = Quaternion.identity;

        Fade(1, 0.2f);
    }
    Vector3 randomForce(string colliderName)
    {
        if (colliderName == "Ceil")
        {
            return new Vector3(Random.Range(-100, 100), 0);
        }
        if (colliderName == "Floor")
        {
            return new Vector3(Random.Range(-50, 50), Random.Range(300, 350));
        }
        return Vector3.zero;
    }
    public void DeactiveFeverSprite()
    {
        //setSprite();
        darkBall.DOFade(0, 0.5f);
        darkWing1.DOFade(0, 0.5f);
        darkWing2.DOFade(0, 0.5f).OnComplete(() => darkMode.SetActive(false));
    }
    public void ActiveFeverSprite()
    {
        darkMode.SetActive(true);
        darkBall.DOFade(1, 0.01f);
        darkWing1.DOFade(1, 0.01f);
        darkWing2.DOFade(1, 0.01f);

    }
    public void ActiveParticle(int swish)
    {
        if (swish >= 2)
        {
            ActiveFeverSprite();
            flame.Play();
            smoke.Stop();
        }
        else if (swish == 1)
        {
            smoke.Play();
            flame.Stop();
        }
        else
        {
            flame.Stop();
            smoke.Stop();
        }
    }
    public void DeactiveParticle()
    {
        DeactiveFeverSprite();
        flame.Stop();
        smoke.Stop();
    }
    public void Fade(float endValue, float time)
    {
        frontWing.GetComponent<SpriteRenderer>().DOKill();
        backWing.GetComponent<SpriteRenderer>().DOKill();
        gameObject.GetComponent<SpriteRenderer>().DOKill();
        frontWing.GetComponent<SpriteRenderer>().DOFade(endValue, time).SetUpdate(true);
        backWing.GetComponent<SpriteRenderer>().DOFade(endValue, time).SetUpdate(true);
        gameObject.GetComponent<SpriteRenderer>().DOFade(endValue, time).SetUpdate(true);
    }
    public bool IsMouseOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }

        return false;
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
