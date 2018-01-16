using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallPillScript : MonoBehaviour
{
    public GameObject ball;

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
            Instantiate(ball, transform.position, Quaternion.identity);
            Instantiate(ball, transform.position, Quaternion.identity);
            Instantiate(ball, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
