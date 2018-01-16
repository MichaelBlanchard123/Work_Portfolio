using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;

public class EditorManager : MonoBehaviour {

    //MULTI FUNCTION VARIABLES\\
    public Sprite selected;
    public Sprite unselected;
    public GameObject editorpillparent;
    public GameObject editorpillcolorpanel;
    public AppManager appmanager;
    public pill_data[] pill_data;
    public static bool pillediting = true;

    private int currentindex = 0;

    //RAYCAST PILL EDITING\\
    void Update()
    {
        if (Input.GetMouseButton(0) && !filepaneltoggle.isOn && !uipaneltoggle.isOn && pillediting)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.transform.IsChildOf(editorpillparent.transform))
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = AppManager.Instance.pilltypeandcolor[(currentlyselectedpill * 7) + currentlyselectedcolor]; 
                    //index out of range when changing pill screen out of scope
                }
            }
        }
    }

    //START\\
    void Start()
    {
        SelectColor(0);
        SelectType(0);
        ChangeLives(2);
    }

    //LEFT ARROW\\
    public void LeftArrow()
    {
        if (currentindex > 1)
        {
            currentindex -= 12;
            SelectType(pilltypex);
            ChangeSprites();
        }
    }

    //RIGHT ARROW\\
    public void RightArrow()
    {
        if (currentindex < 11)
        {
            currentindex += 12;
            SelectType(pilltypex);
            ChangeSprites();
        }
    }

    //EDITOR PILL SPRITE CHANGES\\
    public GameObject[] pillslots;
    public Sprite lockedpill;

    public void ChangeSprites()
    {
        for (int i = 0; i < 12; i++)
        {
            try
            {
                pillslots[i].GetComponent<Image>().sprite = AppManager.Instance.pilltypeandcolor[(currentindex + i) * 7 + currentlyselectedcolor];
                pillslots[i].SetActive(true);

                if(AppManager.Instance.player_data.progress > currentindex + i)
                {
                    pillslots[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                else
                {
                    pillslots[i].GetComponent<Image>().sprite = lockedpill;
                    pillslots[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
                }
            }
            catch
            {
                pillslots[i].SetActive(false);
            }
        }
    }

    //STARTING LIVES SPRITE CHANGES\\
    private int currentlyselectedlives;
    public GameObject[] startinghearts;
    public Sprite fullheart;
    public Sprite emptyheart;
    
    public void ChangeLives(int i)
    {
        currentlyselectedlives = i + 1;

        for(int j = 0; j < 5; j++)
        {
            if (j <= i)
                startinghearts[j].GetComponent<Image>().sprite = fullheart;
            else
                startinghearts[j].GetComponent<Image>().sprite = emptyheart;
        }
    }

    //TOGGLE NO COLOR PANEL\\
    private int[] nocolorpills = { 1, 2, 5, 10, 20 }; //pills that dont require color panel

    public void ToggleNoColor()
    {
        editorpillcolorpanel.SetActive(true);

        foreach (int pillindex in nocolorpills)
        {
            if (currentlyselectedpill == pillindex)
                editorpillcolorpanel.SetActive(false);
        }
    }

    //SELECT PILL TYPE\\
    private int currentlyselectedpill;
    private int pilltypex;

    public void SelectType(int x)
    {
        int tempindex = currentindex;

        if (x + currentindex < AppManager.Instance.player_data.progress)
        {
            pilltypex = x;
            currentlyselectedpill = currentindex + x;
            pillslots[x].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = selected;

            ToggleNoColor();

            tempindex = 0;
        }

        for (int i = 0; i < 12; i++)
        {
            if (i != x + tempindex)
                pillslots[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = unselected;
        }
    }

    //SELECT PILL COLOR\\
    public Image[] pillcolorhightlighter;
    private int currentlyselectedcolor;

    public void SelectColor(int y)
    {
        currentlyselectedcolor = y;
        pillcolorhightlighter[y].sprite = selected;

        ChangeSprites();

        for (int i = 0; i < 7; i++)
        {
            if (i != y)
                pillcolorhightlighter[i].sprite = unselected;
        }
    }

    //CLEAR LEVEL\\
    public void ClearLevel()
    {
        // ADD YES/NO TEXTBOX HERE
        foreach (Transform child in editorpillparent.transform)
            child.gameObject.GetComponent<SpriteRenderer>().sprite = AppManager.Instance.pilltypeandcolor[7];
    }

    //TAKE SCREENSHOT\\
    public Transform editor;
    public GameObject background;

    IEnumerator TakeScreenshot(string lvlname)
    {
        string filePath = ""; // AppManager.GetDataPathForPlatform() + "/Thumbnail/" + lvlname + "_thumb.png";
        int screenheight = Screen.height;
        int screenwidth = Screen.width;

        background.gameObject.SetActive(false);
        foreach (Transform child in editor)
        {
            child.gameObject.SetActive(false);
        }

        Screen.SetResolution(640, 480, true);

        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        tex.Apply();

        int i = 0;
        foreach (Transform child in editor)
        {
            child.gameObject.SetActive(true);
            if (i > 3)
                break;
            i++;
        }
        background.gameObject.SetActive(true);

        Screen.SetResolution(screenwidth, screenheight, true);

        appmanager.TextBox(1, "saved", "successfully saved level to custom tab.");
        File.WriteAllBytes(filePath, tex.EncodeToPNG());
    }

    //LOAD JSON FOR EDIT\\
    public void EditLevelFromLevelSelect()
    {
        levelnametext.text = LevelSelectManager.currently_selected.levelname;
        //descriptiontext.text = LevelSelectManager.currently_selected.leveldescription;

        //remove the exit button and stop user from inputting a different levelname
        //when clicking save button send the user back to the leveldetail screen.
        //delete old screenshot and take new one.
    }

    //LOAD JSON ON ENABLE\\
    void OnEnable()
    {
        int l = 0;

        string jsonString = AppManager.GetJsonDataByPlatform("/Json/preview_Data.json", "read");
        pill_data = JsonHelper.FromJson<pill_data>(jsonString);

        foreach (Transform child in editorpillparent.transform)
        {
            child.gameObject.transform.position = new Vector2(pill_data[l].x, pill_data[l].y);
            child.gameObject.GetComponent<SpriteRenderer>().sprite = AppManager.Instance.pilltypeandcolor[pill_data[l].index];
            l++;
        }

        AppManager.Instance.Tooltips(1);
    }

    //SAVE JSON ON DISABLE EDITOR\\ 
    public void OnDisableToMenu() //+++ LAG +++ take this out if too much lag occurs on mobile.
    {
        SavingLevelJson("preview");
    }

    //APPEND LEVELS JSON\\
    public void AppendingLevelsJson()
    {
        string jsonString = AppManager.GetJsonDataByPlatform("/custom_levels_info.json", "read");

        string newdata = "{\"levelname\": \"" + levelnametext.text +
            "\",\"leveldescription\": \"" + descriptiontext.text +
            "\",\"index\":" + 0;

        if (jsonString.IndexOf("]}") == 10)
            newdata = newdata + "}";
        else
            newdata = newdata + "},";

        string newjsonstring = jsonString.Insert(10, newdata);

        File.WriteAllText(Application.streamingAssetsPath + "/custom_levels_info.json", newjsonstring);
    }

    //LEVEL VALIDATION\\
    public InputField levelnametext;
    public InputField descriptiontext;
    public Toggle filepaneltoggle;
    public Toggle uipaneltoggle;

    public void LevelValidation()
    {
        string levelname = levelnametext.text;
        bool validationpass = true;

        //levelname = levelname.Replace(' ', '_'); //replace spaces with underscores  MAY NOT USE IF TO MUCH WORK

        if (levelname != "")
        {
            for (int y = 0; y < AppManager.classic_levels_info.Length; y++)
            {
                if (levelname == AppManager.classic_levels_info[y].levelname)
                    validationpass = false;
            }
            for (int x = 0; x < AppManager.custom_levels_info.Length; x++)
            {
                if (levelname == AppManager.custom_levels_info[x].levelname) // ADD YES/NO TEXTBOX HERE FOR OVERWRITE
                    validationpass = false;
            }

            if (validationpass)
            {
                SavingLevelJson(levelname);
                AppendingLevelsJson();
            }
                
            else
                appmanager.TextBox(2, "error", "please enter a valid name! level name is alreayd takken.");
        }
        else
            appmanager.TextBox(2, "error", "please enter a valid name! level name is blank.");
    }

    //SAVING JSON LEVEL\\
    public void SavingLevelJson(string name)
    {
        int i = 0;
        string path = Application.streamingAssetsPath + "/Json/" + name + "_Data.json";

        if (!File.Exists(path))
        {
            FileStream file = File.Create(path);
            file.Close();
        }
        else
        {
            FileStream file = File.Open(path, FileMode.Open);
            file.Close();
        }

        foreach (Transform child in editorpillparent.transform)
        {
            pill_data[i].x = child.gameObject.transform.position.x;
            pill_data[i].y = child.gameObject.transform.position.y;
            pill_data[i].index = Array.IndexOf(AppManager.Instance.pilltypeandcolor, child.gameObject.GetComponent<SpriteRenderer>().sprite);
            i++;
        }
        string json_data = JsonHelper.ToJson(pill_data, true);

        File.WriteAllText(path, json_data);

        if (name == "preview") //take this check out
            PlayerPrefs.SetString("levelname", name);
        else
            StartCoroutine("TakeScreenshot", name);
    }
}