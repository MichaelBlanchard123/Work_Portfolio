using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelPillScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Doesn't destroy because it's steel
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
