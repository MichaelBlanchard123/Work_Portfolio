using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeramicPillScript : MonoBehaviour {

    public Sprite[] Ceramic_sprites;

    private int PillHealth;

    void Start()
    {
        PillHealth = 1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnTrigger(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PillHealth = 4;
        OnTrigger(other.gameObject);
    }

    private void OnTrigger(GameObject other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Ball Collider")
        {
            if (PillHealth == 1)
                gameObject.GetComponent<SpriteRenderer>().sprite = Ceramic_sprites[0];
            else if (PillHealth == 2)
                gameObject.GetComponent<SpriteRenderer>().sprite = Ceramic_sprites[1];
            else if (PillHealth == 3)
                gameObject.GetComponent<SpriteRenderer>().sprite = Ceramic_sprites[2];
            else if (PillHealth == 4)
                Destroy(gameObject);

            PillHealth++;
        }
    }
}
