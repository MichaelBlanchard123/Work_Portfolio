using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform launcharrow;
    public float MinimumHorizontalMovement = 0.4F;

    private float MinimumSpeed = 6.5f;
    private float MaximumSpeed = 6.5f;
    private bool toggle;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        toggle = false;

        //Multi-ball random direction
        if (GameManager.hasBeenLaunched || BackgroundAnimation.haslaunched)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1.0F, 1.0F), Mathf.Abs(Random.value));
            GetComponent<CircleCollider2D>().enabled = true;

            randomDirection = randomDirection.normalized * MinimumSpeed;

            rb.velocity = randomDirection;
        }
    }

    void Update()
    {
        if (GameManager.hasBeenLaunched)
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
        else if (Input.GetButtonDown("Fire1") && !GameManager.hasBeenLaunched || BackgroundAnimation.haslaunched)
        {
            rb.velocity = launcharrow.transform.position - transform.position;

            if(!BackgroundAnimation.haslaunched)
                Destroy(launcharrow.gameObject);

            GetComponent<CircleCollider2D>().enabled = true;
            transform.parent = null;

            GameManager.hasBeenLaunched = true;
        }
        else
        {
            if (launcharrow.transform.rotation.z < .50 && !toggle)
                launcharrow.transform.RotateAround(transform.position, new Vector3(0, 0, 90), 60 * Time.deltaTime);
            else
                toggle = true;

            if (launcharrow.transform.rotation.z > -.50 && toggle)
                launcharrow.transform.RotateAround(transform.position, new Vector3(0, 0, -90), 60 * Time.deltaTime);
            else
                toggle = false;
        }
    }
}