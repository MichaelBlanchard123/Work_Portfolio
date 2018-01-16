using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBall : MonoBehaviour {

    public float MinimumHorizontalMovement = 0.4F;
    private float MinimumSpeed = 35f;
    private float MaximumSpeed = 35f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject[] tracerballs = GameObject.FindGameObjectsWithTag("GuideBall");

        foreach (GameObject ball in balls)
        {
            Physics2D.IgnoreCollision(ball.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }

        foreach (GameObject tracerball in tracerballs)
        {
            Physics2D.IgnoreCollision(tracerball.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }
    }

    void Update()
    {
        Vector2 direction = rb.velocity;
        float speed = direction.magnitude;
        direction.Normalize();

        if (direction.y > -MinimumHorizontalMovement && direction.y < MinimumHorizontalMovement)
        {
            direction.y = direction.y < 0 ? -MinimumHorizontalMovement : MinimumHorizontalMovement;

            direction.x = direction.x < 0 ? -1 + MinimumHorizontalMovement : 1 - MinimumHorizontalMovement;


            rb.velocity = direction * speed;
        }

        if (speed < MinimumSpeed || speed > MaximumSpeed)
        {
            speed = Mathf.Clamp(speed, MinimumSpeed, MaximumSpeed);

            rb.velocity = direction * speed;
        }
    }
}
