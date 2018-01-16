using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHeartPillScript: MonoBehaviour {

    public GameObject oneUpAnimation;

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
                Instantiate(oneUpAnimation, transform.position, Quaternion.identity, gameObject.transform);
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(gameObject, 3f);
                GameManager.lives++;
                GameManager.ChangeLifeAmount();
            }
            catch
            {
            
            }
        } 
    }
}
