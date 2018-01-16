using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerPillScript : MonoBehaviour {

    public GameObject freezerAnimation;
    public GameObject standardpill;
    public Sprite frozensprite;

    private int i = 0;

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
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 1.6f);

            //Instantiate(freezerAnimation, transform.position, Quaternion.identity);

            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "Pill" && hitColliders[i].gameObject.name != "Freezer Pill(Clone)")
                {
                    int range = (int)Mathf.Round(Vector2.Distance(hitColliders[i].gameObject.transform.position, gameObject.transform.position) * 2);
                    int value = Random.Range(0, range);

                    if(value == Random.Range(0, range))
                    {
                        GameObject current = Instantiate(standardpill, hitColliders[i].gameObject.transform.position, Quaternion.identity, AppManager.Instance.currentparent);
                        current.GetComponent<SpriteRenderer>().sprite = frozensprite;
                        Destroy(hitColliders[i].gameObject);
                    }
                }
                i++;
            }

            Destroy(gameObject);
        }
    }
}
