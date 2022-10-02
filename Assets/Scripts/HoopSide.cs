using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopSide : MonoBehaviour
{
    [SerializeField] HoopController hoopController;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hoopController.IsSwish = false;
            //if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude>0.5f)
            if (collision.relativeVelocity.magnitude>2f)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity= new Vector2(0.5f,collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            if (collision.relativeVelocity.magnitude > 1f)
                AudioManager.Instance.Play("Bounce");
            if (GameManager.mode == GameMode.Challenge)
            {
                this.PostEvent(EventID.OnCollideWithHoop);
                ChallengeController.Instance.SwishCount = 0;
                ChallengeController.Instance.DeactiveParticle();
            }
            else
            {
                GameController.instance.SwishCount = 0;
                GameController.instance.DeactiveParticle();
            }
        }
    }
}
