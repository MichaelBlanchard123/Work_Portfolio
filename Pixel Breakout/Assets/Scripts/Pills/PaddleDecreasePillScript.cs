using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleDecreasePillScript : MonoBehaviour {

    //START\\
    string scenename;

    void Start()
    {
        scenename = SceneManager.GetActiveScene().name;
    }

    //CHANGE PADDLE LENGHT\\
    public Sprite[] paddles;
    public float[] colliderxdata;

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
            GameObject paddle;

            if (scenename == "Main Menu")
                paddle = BackgroundAnimation.Instance.paddle;
            else
                paddle = GameManager.Instance.cpaddle;

            for (int i = 0; i < 4; i++)
            {
                if (paddles[i] == paddle.GetComponent<SpriteRenderer>().sprite)
                {
                    paddle.GetComponent<SpriteRenderer>().sprite = paddles[i + 1];
                    paddle.GetComponent<BoxCollider2D>().size = new Vector2(colliderxdata[i + 1], 0.48f);
                    break;
                }
            }

            Destroy(gameObject);
        }
    }
}
