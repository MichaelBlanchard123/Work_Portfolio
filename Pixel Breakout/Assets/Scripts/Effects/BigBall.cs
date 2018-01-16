using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBall : MonoBehaviour {

    public Sprite regularballsprite;
    public GameObject[] balls;

    void Start ()
    {
        AppManager.Instance.statuseffects[0] = 15;
        StartCoroutine(BigBallTimeout());
    }

    IEnumerator BigBallTimeout()
    {
        while (AppManager.Instance.statuseffects[0] >= 0)
        {
            yield return new WaitForSeconds(1f);
            AppManager.Instance.statuseffects[0]--;
        }

        balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            ball.GetComponent<SpriteRenderer>().sprite = regularballsprite;
            ball.GetComponent<CircleCollider2D>().radius = 0.12f;
            ball.transform.GetChild(0).GetComponent<CircleCollider2D>().radius = 0.12f;
        }

        Destroy(GetComponent<BigBall>());
    }
}
