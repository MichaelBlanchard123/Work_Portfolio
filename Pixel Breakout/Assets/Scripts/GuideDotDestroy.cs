using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideDotDestroy : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Destroy(gameObject);
        }

        Destroy(gameObject, 1f);
    }
}
