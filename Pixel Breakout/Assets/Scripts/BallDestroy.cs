using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroy : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject);

            if (GameObject.FindGameObjectsWithTag("Ball").Length == 1 && !BackgroundAnimation.haslaunched)
            {
                GameManager.Instance.LoseLife();
            }
        }
    }
}
