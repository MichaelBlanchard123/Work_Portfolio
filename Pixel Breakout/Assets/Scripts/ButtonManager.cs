using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    //public static IEnumerator WaitInput(bool wait, string control, Text t)
    //{
    //    while (wait)
    //    {
    //        if (Input.anyKeyDown)
    //        {
    //            KeyCode pressed = (KeyCode)Enum.Parse(typeof(KeyCode), Input.inputString, true);
    //            print(pressed);
    //            wait = false;
    //        }
    //        yield return null;
    //    }
    //}

    public void YesTextBox()
    {
        EditorManager.pillediting = true;
        //AppManager.clickedyes = true;
        Destroy(gameObject);
    }

    public void CloseTextBox()
    {
        EditorManager.pillediting = true;
        Destroy(gameObject);
    }
}
