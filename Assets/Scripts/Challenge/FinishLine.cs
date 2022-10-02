using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] BallController ball;
    bool isJumping, isTouched;
    [SerializeField] ParticleSystem[] startEffect;
    void Start()
    {
        isTouched = false;
        ball = GameObject.FindGameObjectWithTag("Player").GetComponent<BallController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ChallengeController.Instance.IsGameOver == false)
        {
            playStarEffect();
            isTouched = true;
            AudioManager.Instance.Play("Cheer");
            UI_Challenge.Instance.CompleteLevel();
            this.PostEvent(EventID.OnCompleteChallenge);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (ball.transform.position.y < 0.0f)
        {
            if (isJumping == false && isTouched == true)
            {
                isJumping = true;
                ball.Jump();
                StartCoroutine(AfterJump(0.4f));
            }
        }
    }
    IEnumerator AfterJump(float time)
    {
        yield return new WaitForSeconds(time);
        isJumping = false;
    }
    void playStarEffect()
    {
        foreach (ParticleSystem p in startEffect)
            p.Play();
    }
    IEnumerator AfterEffect(float time, ParticleSystem p)
    {
        yield return new WaitForSeconds(time);
        p.Play();
    }
}
