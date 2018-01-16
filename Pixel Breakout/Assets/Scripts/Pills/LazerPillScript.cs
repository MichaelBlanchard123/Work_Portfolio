using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPillScript : MonoBehaviour
{
    public GameObject[] balls;

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
            if ((AppManager.Instance.gameObject.GetComponent("Lazer") as Lazer) != null)
                AppManager.Instance.statuseffects[7] = 5;
            else
                AppManager.Instance.gameObject.AddComponent<Lazer>();

            Destroy(gameObject);
        }
    }
}
