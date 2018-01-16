using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPillScript : MonoBehaviour
{
    public GameObject target;
    private bool trigger = false;

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
            if (!trigger) //might not need
            {
                trigger = true;
                Instantiate(target, new Vector2(0, 0), Quaternion.identity, AppManager.Instance.currentparent);

                Destroy(gameObject);
            }
        }
    }
}
