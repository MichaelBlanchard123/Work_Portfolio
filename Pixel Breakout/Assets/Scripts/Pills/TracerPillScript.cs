using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TracerPillScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        OnTrigger(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other.gameObject);
    }

    private Tracer tracerscript;
    public GameObject tracerdot;
    public GameObject tracerball;

    private void OnTrigger(GameObject other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Ball Collider")
        {
            if (other.gameObject.tag != "GuideBall")
            {
                if ((AppManager.Instance.gameObject.GetComponent("Tracer") as Tracer) != null)
                    AppManager.Instance.statuseffects[2] = 15;
                else
                {
                    tracerscript = AppManager.Instance.gameObject.AddComponent<Tracer>() as Tracer;
                    tracerscript.GetComponent<Tracer>().guideball = tracerball;
                    tracerscript.GetComponent<Tracer>().guidedot = tracerdot;
                }
                Destroy(gameObject);
            }
        }
    }
}
