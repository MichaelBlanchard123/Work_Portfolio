using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MysteryPillScript : MonoBehaviour
{
    private int randomValue;
    int index;

    //START\\
    private void Start()
    {
        while (true)
        {
            randomValue = UnityEngine.Random.Range(0, AppManager.Instance.player_data.progress);
            if (randomValue != 1 && randomValue != 8)
                break;
        }
        index = Array.IndexOf(AppManager.Instance.pilltypeandcolor, GetComponent<SpriteRenderer>().sprite);
        index = index % 7;

        if (AppManager.Instance.pilltypeandcolor[index + (randomValue * 7)] == null)
            index = 0;
        
    }

    //COLLISION ENTER\\
    private bool trigger = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnTrigger(other.gameObject, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other.gameObject, false);
    }

    private void OnTrigger(GameObject other, bool trigger1)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Ball Collider")
        {
            if (!trigger)
            {
                trigger = true;
                StartCoroutine(RandomPill(trigger1));
            }
        }
            
    }

    //RANDOM PILL SPAWNING AMD ANIMATION\\
    IEnumerator RandomPill(bool trigger1)
    {
        GameObject current;
        current = Instantiate(AppManager.Instance.pilltypes[randomValue], new Vector2(transform.position.x, transform.position.y), Quaternion.identity, AppManager.Instance.currentparent);
        current.GetComponent<SpriteRenderer>().sprite = null;
        current.GetComponent<BoxCollider2D>().enabled = false;

        for (int i = 0; i < 20; i++)
        {
            if(trigger1)
            {
                int randomValuetemp;
                randomValuetemp = UnityEngine.Random.Range(0, AppManager.Instance.player_data.progress);

                if (randomValuetemp == 1 || randomValuetemp == 8)
                    continue;

                GetComponent<SpriteRenderer>().sprite = AppManager.Instance.pilltypeandcolor[index + (randomValuetemp * 7)];
                yield return new WaitForSeconds(0.06f);
            }
        }
        
        current.GetComponent<SpriteRenderer>().sprite = AppManager.Instance.pilltypeandcolor[index + (randomValue * 7)];
        current.GetComponent<BoxCollider2D>().enabled = true;
        Destroy(gameObject);
    }
}
