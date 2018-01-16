using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour {

    //MULTI FUNCTION VARIABLES\\
    private int current_indexcl = 0;
    private int current_indexcu = 0;

    //START\\
    void Start()
    {
        UpdateOnClick();
    }

    //ON ENABLE\\
    void OnEnable()
    {
        if(!isClassic)
        {
            UpdateOnClick();
        }
    }

    //TOGGLE LEVEL TYPE\\
    private bool isClassic = true;

    public void ToggleLevelType(bool x)
    {
        isClassic = x;
    }

    //RIGHT ARROW\\
    public void RightArrow()
    {
        if (isClassic)
        {
            if (current_indexcl < AppManager.classic_levels_info.Length - 8)
                current_indexcl += 8;
        }       
        else if (!isClassic)
        {
            if (current_indexcu < AppManager.custom_levels_info.Length - 8)
                current_indexcu += 8;
        }
        UpdateOnClick();
    }

    //LEFT ARROW\\
    public void LeftArrow()
    {
        if(isClassic)
        {
            if (current_indexcl > 1)
                current_indexcl -= 8;
        }
        else if (!isClassic)
        {
            if (current_indexcu > 1)
                current_indexcu -= 8;
        }
        UpdateOnClick();
    }

    //SELECT LEVEL\\
    public Text detailtitle;
    public Text detaildescription;
    public Image detailimagepreview;

    public void SelectLevel(int x)
    {
        WWW www = FindImage(x);

        detailimagepreview.sprite = Sprite.Create(www.texture as Texture2D, new Rect(0f, 0f, www.texture.width, www.texture.height), Vector2.zero);
        detailtitle.text = currently_selected.levelname;
        //detaildescription.text = currently_selected.leveldescription;
    }

    //DELETE LEVEL\\
    public void DeleteLevel() //function could use some refining.
    {
        string path;
        int j = 0;
        int k = 2;

        path = Application.streamingAssetsPath + "/Thumbnail/" + currently_selected.levelname + "_thumb.png";
        File.Delete(path);
        path = Application.streamingAssetsPath + "/Json/" + currently_selected.levelname + "_Data.json";
        File.Delete(path);
        path = Application.streamingAssetsPath + "/MenuJson/custom_levels_info.json";

        string jsonString = File.ReadAllText(path);

        int x = jsonString.IndexOf(currently_selected.levelname);
        x = x - 15;

        if (jsonString.IndexOf("},", x) == -1 && jsonString.IndexOf("[", x - 3) == -1)
            j = 1;

        if (AppManager.custom_levels_info.Length == 1)
            k = 1;

        jsonString = jsonString.Remove(x - j, jsonString.IndexOf("}", x) - (x - k));
        File.WriteAllText(path, jsonString);
        UpdateOnClick();
    }

    //FIND THUMB IMAGE\\
    public static levels_info currently_selected;

    public WWW FindImage(int index)
    {
        string path;

        if (isClassic)
            currently_selected = AppManager.classic_levels_info[current_indexcl + index];
        else
            currently_selected = AppManager.custom_levels_info[current_indexcu + index];

        path = Application.streamingAssetsPath + "/Thumbnail/" + currently_selected.levelname + "_thumb.png";

        if (!File.Exists(path))
            path = Application.streamingAssetsPath + "/Thumbnail/No_Image_Preview.png";

        #if UNITY_EDITOR || UNITY_STANDALONE
        path = "File://" + path;
        #endif

        WWW www = new WWW(path);
        StartCoroutine(AppManager.WaitForRequest(www));

        return www;
    }

    //UPDATE LEVEL ON CLICK\\
    public GameObject[] levelslots;
    public GameObject nomaps;
    public Sprite lockedlevel;
    public Sprite unlockedlevel;

    public void UpdateOnClick()
    {
        AppManager.RefreshCustomJson(); //+++ LAG +++ take this out if too much lag occurs on mobile.
        nomaps.gameObject.SetActive(false);

        if (!isClassic && AppManager.custom_levels_info.Length == 0)
            nomaps.gameObject.SetActive(true);

        for (int i = 0; i < 8; i++)
        {
            try
            {
                WWW www = FindImage(i);

                levelslots[i].GetComponentInChildren<Text>().text = currently_selected.levelname;
                levelslots[i].GetComponent<Image>().sprite = Sprite.Create(www.texture as Texture2D, new Rect(0f, 0f, www.texture.width, www.texture.height), Vector2.zero);
                levelslots[i].SetActive(true);

                if (isClassic && AppManager.Instance.player_data.progress <= i + current_indexcl)
                {
                    levelslots[i].transform.GetChild(0).GetComponent<Image>().sprite = lockedlevel;
                    levelslots[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
                }
                else
                {
                    levelslots[i].transform.GetChild(0).GetComponent<Image>().sprite = unlockedlevel;
                    levelslots[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
            }
            catch
            {
                levelslots[i].SetActive(false);
            }
        }
    }
}