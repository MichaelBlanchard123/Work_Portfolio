using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTPillScript : MonoBehaviour {

    private int i = 0;

    public GameObject explosionAnimation;
    public bool explosionchain;

    public Sprite tntregular;
    public Sprite tntlighter;

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnTrigger(other.gameObject, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other.gameObject, false);
    }

    private void OnTrigger(GameObject other, bool trigger)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Ball Collider")
            StartCoroutine(Explosion(trigger));
    }

    public void Chain() //review for efficency
    {
        StartCoroutine(Explosion(true));
    }

    IEnumerator Explosion(bool trigger)
    {
        for (int i = 0; i < 4; i++)
        {
            if(trigger)
            {
                GetComponent<SpriteRenderer>().sprite = tntlighter;
                yield return new WaitForSeconds(0.13f);
                GetComponent<SpriteRenderer>().sprite = tntregular;
                yield return new WaitForSeconds(0.13f);
            }
        }
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 0.8f);

        Instantiate(explosionAnimation, transform.position, Quaternion.identity);

        while (i < hitColliders.Length)
        {
            if(hitColliders[i].gameObject.name == "TNT Pill(Clone)")
            {
                TNTPillScript script = hitColliders[i].gameObject.GetComponent<TNTPillScript>();
                script.Chain();
            }
            else if(hitColliders[i].gameObject.tag != "Untagged")
            {
                Destroy(hitColliders[i].gameObject);
            }
            i++;
        }

        Destroy(gameObject);
    }
}