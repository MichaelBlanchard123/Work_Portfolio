using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    //UPDATE PADDLE MOVEMENT TYPE\\
    public float paddleSpeed = 0.3f;
    private Vector2 playerPos = new Vector2(0, -4);

    void Update()
    {
        if(AppManager.Instance.player_data.movementtype == "keyboard")
        {
            float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
            playerPos = new Vector2(Mathf.Clamp(xPos, -8.7f, 8.7f), -4f);

            transform.position = playerPos;
        }
        else if(AppManager.Instance.player_data.movementtype == "drag")
        {
            //DRAG CODE HERE
        }
        else if (AppManager.Instance.player_data.movementtype == "gyroscopic")
        {
            //GYROSCOPIC CODE HERE
        }
        else if (AppManager.Instance.player_data.movementtype == "screen tap")
        {
            //SCREEN TAP CODE HERE
        }
    }
}
