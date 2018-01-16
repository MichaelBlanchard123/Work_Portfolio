using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public GameObject[] threepills;
    private bool coroutineEnd = false;

    void Start ()
    {
        StartCoroutine(GetTargets());
    }

    IEnumerator GetTargets()
    {
        yield return 0; // wait till end of frame

        int i = 0;
        int index = 0;

        while (true)
        {
            int number = Random.Range(0, AppManager.Instance.currentparent.childCount);

            if (AppManager.Instance.currentparent.GetChild(number).tag == "Pill")
            {
                threepills[i] = AppManager.Instance.currentparent.GetChild(number).gameObject;
                index = 0;
                i++;
            }
            index++;

            if (i > 2 || index > 319)
                break;
        }
        coroutineEnd = true;
    }

    private int f = 0;
    private float timer = 0;

    void Update ()
    {
        if (coroutineEnd)
        {
            if (f < 3)
            {
                if (threepills[f] != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, threepills[f].transform.position, 5 * Time.deltaTime);

                    if (Vector3.Distance(transform.position, threepills[f].transform.position) <= 0)
                    {
                        timer += Time.deltaTime;

                        if (timer > 1f)
                        {
                            transform.GetChild(0).transform.GetComponent<Animator>().SetTrigger("FlashTrigger");
                            Destroy(threepills[f].gameObject);

                            timer = 0;
                            f++;
                        }
                    }
                }
                else
                    f++;
            }
            else
                Destroy(gameObject, 0.4f);
        }
    }
}