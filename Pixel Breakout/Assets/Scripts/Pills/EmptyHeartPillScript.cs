using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyHeartPillScript: MonoBehaviour {

    public GameObject oneDownAnimation;

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
            try
            {
                Instantiate(oneDownAnimation, transform.position, Quaternion.identity, gameObject.transform);
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(gameObject, 3f);
                GameManager.lives--;
                GameManager.ChangeLifeAmount();
            }
            catch
            {

            }
        }
    }
}
