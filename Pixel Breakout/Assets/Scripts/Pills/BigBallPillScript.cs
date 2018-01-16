using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBallPillScript : MonoBehaviour
{
    public GameObject[] balls;
    public Sprite large_ball_sprite;
    public Sprite regular_ball_sprite;

    private BigBall bigballscript;

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnTrigger(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other.gameObject);
    }

    private void OnTrigger(GameObject other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Ball Collider")
        {
            balls = GameObject.FindGameObjectsWithTag("Ball");

            foreach (GameObject ball in balls)
            {
                ball.GetComponent<SpriteRenderer>().sprite = large_ball_sprite;
                ball.GetComponent<CircleCollider2D>().radius = 0.21f;
                ball.transform.GetChild(0).GetComponent<CircleCollider2D>().radius = 0.21f;
            }

            if ((AppManager.Instance.gameObject.GetComponent("BigBall") as BigBall) != null)
            {
                AppManager.Instance.statuseffects[0] = 15;
            }
            else
            {
                bigballscript = AppManager.Instance.gameObject.AddComponent<BigBall>() as BigBall;
                bigballscript.GetComponent<BigBall>().regularballsprite = regular_ball_sprite;
            }

            Destroy(gameObject);
        }
    }
}
