using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimation : MonoBehaviour {

    public GameObject ball;
    public GameObject paddle;
    public static bool haslaunched;

    private Vector2 ballPos = new Vector2(0, -4);
    private Rigidbody2D ballrb;

    //INSTANCE\\
    public static BackgroundAnimation Instance;

    void Awake()
    {
        Instance = this;
    }

    //START\\
    void Start()
    {
        SpawnBackgroundPills();

        //ball
        ballrb = ball.GetComponent<Rigidbody2D>();

        Vector2 randomDirection = new Vector2(Random.Range(-1.0F, 1.0F), Mathf.Abs(Random.value));
        ball.GetComponent<CircleCollider2D>().enabled = true;
        randomDirection = randomDirection.normalized * 6.5f;
        ballrb.velocity = randomDirection;
    }

    //TURN OFF BALL AUTO LAUNCH
    void OnDisable()
    {
        haslaunched = false;
    }

    //TURN ON BALL AUTO LAUNCH
    void OnEnable()
    {
        haslaunched = true;
    }

    //PADDLE AND BALL UPDATE\\
    void Update()
    {
        try
        {
            //ball
            Vector2 direction = ballrb.velocity;
            float speed = direction.magnitude;
            direction.Normalize();

            if (direction.y > -0.4f && direction.y < 0.4f)
            {
                direction.y = direction.y < 0 ? -0.4f : 0.4f;
                direction.x = direction.x < 0 ? -1 + 0.4f : 1 - 0.4f;

                ballrb.velocity = direction * speed;
            }

            if (speed != 6.5f)
            {
                speed = 6.5f;
                ballrb.velocity = direction * speed;
            }

            //paddle
            float xPos = ball.transform.position.x;
            ballPos = new Vector2(Mathf.Clamp(xPos, -9.3f, 9.3f), paddle.transform.position.y);
            paddle.transform.position = ballPos;
        }
        catch
        {

        }
    }

    //MENU PILLS SPAWNING\\
    public Transform menupills;
    
    public void SpawnBackgroundPills()
    {
        pill_data[] pill_data;

        string jsonString = AppManager.GetJsonDataByPlatform("/MenuJson/menu_Data.json", "read");
        pill_data = JsonHelper.FromJson<pill_data>(jsonString);

        for (int i = 0; i < 130; i++)
        {
            int random = Random.Range(0, AppManager.Instance.player_data.progress);

            if (AppManager.Instance.player_data.progress < 6 && random == 3 || random == 1) //reroll if pill is multi-ball and muti -ball is recently unlocked
            {
                i--;
                continue;
            }   

            Instantiate(AppManager.Instance.pilltypes[random], new Vector2(pill_data[i].x, pill_data[i].y), Quaternion.identity, menupills);
        }
    }
}