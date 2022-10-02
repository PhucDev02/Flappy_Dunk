using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopSensor : MonoBehaviour
{
    [SerializeField] HoopController hoopController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //go into from bottom
        if (collision.gameObject.CompareTag("Player"))
        {
            if (AngleBallVsHoop(collision.gameObject.GetComponent<Rigidbody2D>()) < 90)
            {
                collision.GetComponent<Rigidbody2D>().angularDrag = 0;
                GameManager.Instance.gameOver();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.mode == GameMode.Challenge)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (ChallengeController.Instance.IsGameOver == false)
                    if (AngleBallVsHoop(collision.gameObject.GetComponent<Rigidbody2D>()) > 90)
                    {
                        if (hoopController.IsSwish == true)
                        {
                            ChallengeController.Instance.IncreaseSwish();
                            hoopController.ActiveEffect(ChallengeController.Instance.SwishCount);
                        }
                        hoopController.BallPass();
                    }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (GameController.instance.IsGameOver == false)
                    if (AngleBallVsHoop(collision.gameObject.GetComponent<Rigidbody2D>()) > 90)
                    {
                        if (hoopController.IsSwish == true)
                        {
                            GameController.instance.IncreaseSwish();
                            hoopController.ActiveEffect(GameController.instance.SwishCount);
                        }
                        GameController.instance.IncreaseScore();
                        hoopController.BallPass();
                    }
            }
        }
    }
    public float AngleBallVsHoop(Rigidbody2D ball)
    {
        Vector3 upHoop = new Vector3(-Mathf.Sin(hoopController.gameObject.transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(hoopController.gameObject.transform.eulerAngles.z * Mathf.Deg2Rad));
        Vector3 velocity = ball.velocity;
        return Vector3.Angle(upHoop, velocity);
    }
}
