using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {

    public GameObject[] balls;

    void Start()
    {
        AppManager.Instance.statuseffects[7] = 5;
        StartCoroutine(LazerTimeout());
    }

    IEnumerator LazerTimeout()
    {
        while (AppManager.Instance.statuseffects[7] >= 0)
        {
            balls = GameObject.FindGameObjectsWithTag("Ball");
            Physics2D.IgnoreLayerCollision(0, 8, true);

            foreach (GameObject ball in balls)
            {
                ball.transform.GetChild(0).gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(1f);
            AppManager.Instance.statuseffects[7]--;
        }

        balls = GameObject.FindGameObjectsWithTag("Ball");
        Physics2D.IgnoreLayerCollision(0, 8, false);

        foreach (GameObject ball in balls)
        {
            ball.transform.GetChild(0).gameObject.SetActive(false);
        }

        Destroy(GetComponent<Lazer>());
    }
}
