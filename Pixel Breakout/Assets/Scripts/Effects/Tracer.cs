using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer : MonoBehaviour {

    public GameObject guidedot;
    public GameObject guideball;

    private GameObject[] balls;
    private GameObject[] guidedotsarray = new GameObject[15];

    void Start ()
    {
        AppManager.Instance.statuseffects[2] = 15;
        StartCoroutine(DotGenerator());
    }

    IEnumerator DotGenerator()
    {
        while(AppManager.Instance.statuseffects[2] >= 0)
        {
            balls = GameObject.FindGameObjectsWithTag("Ball");

            foreach (GameObject ball in balls)
            {
                StartCoroutine(DotInstance(ball));
            }
            yield return new WaitForSeconds(1f);
            AppManager.Instance.statuseffects[2]--;
        }
        Destroy(GetComponent<Tracer>());
    }

    IEnumerator DotInstance(GameObject ball)
    {
        if (ball != null)
        {
            Rigidbody2D ballrb = ball.GetComponent<Rigidbody2D>(); //get rigidbody for ball

            Vector2 direction = ballrb.velocity; //get direction of ball
            float speed = direction.magnitude;
            direction.Normalize();

            GameObject guideballclone = Instantiate(guideball, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity, AppManager.Instance.currentparent); //Instatiate path marker

            guideballclone.GetComponent<CircleCollider2D>().radius = ball.GetComponent<CircleCollider2D>().radius; //set the radius of guideballclone to ball it is emulating

            Rigidbody2D pathmarkerrb = guideballclone.GetComponent<Rigidbody2D>(); //get ridgidbody of path marker

            pathmarkerrb.velocity = direction * 15; //set velocity of path marker same as ball and speed up travel X100

            for (int j = 0; j < 15; j++) //delete all guide dots
            {
                Destroy(guidedotsarray[j]);
            }

            for (int j = 0; j < 15; j++) //create 15 guide dots for each ball in balls array
            {
                //create guide dot where the path marker is
                if(ball != null)
                    guidedotsarray[j] = Instantiate(guidedot, new Vector2(guideballclone.transform.position.x, guideballclone.transform.position.y), Quaternion.identity, AppManager.Instance.currentparent);
                yield return new WaitForSeconds(0.04f); //time before next guide dot creation
            }
            Destroy(guideballclone);
        }
        yield break;
    }
}
