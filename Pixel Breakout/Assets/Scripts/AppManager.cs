using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour {

    //MULTI FUNCTION VARIABLES\\
    public static levels_info[] classic_levels_info;
    public static levels_info[] custom_levels_info;
    public player_info player_data;
    public Transform currentparent;

    public int[] statuseffects; // 0: Big-Ball     1: Speed     2: Tracer    3: Magnet     4: Bonus X2     5: Bonus X5     6: Bonus X10    7: Lazer

    //sprite data
    public GameObject[] pilltypes;
    public Sprite[] pilltypeandcolor; //update every time new pill is added

    private static string jsonString;
    private string versionnumber;

    //INSTANCE\\
    public static AppManager Instance;

    void Awake()
    {
        Instance = this;
    }

    //START\\
    public AudioMixer audiomixer;
    public Text versiontext;
    public GameObject backgroundpills;
    public GameObject editorpills;

    void Start()
    {
        string scenename = SceneManager.GetActiveScene().name; // get current loaded scene and get parent of pills

        versionnumber = "pre alpha 0.5";
        versiontext.text = versionnumber;

        if (scenename == "Main Menu")
            currentparent = BackgroundAnimation.Instance.menupills;
        else
            currentparent = GameManager.Instance.pillsparent;

        jsonString = GetJsonDataByPlatform("/MenuJson/classic_levels_info.json", "read");
        classic_levels_info = JsonHelper.FromJson<levels_info>(jsonString);

        jsonString = GetJsonDataByPlatform("/MenuJson/custom_levels_info.json", "read");
        custom_levels_info = JsonHelper.FromJson<levels_info>(jsonString);

        jsonString = GetJsonDataByPlatform("/MenuJson/player_info.json", "read");
        player_data = JsonUtility.FromJson<player_info>(jsonString);

        audiomixer.SetFloat("MasterVol", player_data.masterslider);
        audiomixer.SetFloat("MusicVol", player_data.musicslider);
        audiomixer.SetFloat("FxVol", player_data.fxslider);

        Tooltips(0);
        
        //if back from editor preview load editor gameobjects
        if (player_data.editorback)
        {
            canvas.GetChild(0).gameObject.SetActive(false);
            backgroundpills.SetActive(false);
            editorpills.SetActive(true);
            canvas.GetChild(2).gameObject.SetActive(true);

            player_data.editorback = false;
            SavePlayerData();
        }
    }

    //PLATFORM SPECIFIC DATAPATH
    public static string GetJsonDataByPlatform(string path, string type)
    {
        string jsonString = "";
        path = Application.streamingAssetsPath + path;

        if(type == "read")
        {
            if (path.Contains("://"))
            {
                WWW www = new WWW(path);
                while (!www.isDone) { }

                jsonString = www.text;
            }
            else
                jsonString = File.ReadAllText(path);
        }

        //#if UNITY_ANDROID
        //    jsonString = "jar:file://" + Application.dataPath + "!/assets" + path;

        //    WWW reader = new WWW(jsonString);
        //    while (!reader.isDone) { }

        //    jsonString = reader.text;
        //#endif
        //#if UNITY_STANDALONE || UNITY_EDITOR
        //    jsonString = Application.dataPath + "/StreamingAssets" + path;
        //#endif

        return jsonString;
    }

    //TOOLTIP MESSAGES\\
    public void Tooltips(int i)
    {
        if(player_data.tooltips == true)
        {
            if (player_data.progress == 0 && i == 0) //welcome message when progress is 0.
            {
                TextBox(3, "welcome!", "welcome to pixel breakout my first ever mobile game! please rate if you like it. -michael");
                player_data.progress = 1;
            }
            if (player_data.progress < 3 && i == 1)
                TextBox(0, "create at your own risk", "overusing pills may result in application crashes. youve been warned.");
        }
    }

    //REFRESH CUSTOM JSON\\
    public static void RefreshCustomJson()
    {
        jsonString = GetJsonDataByPlatform("/MenuJson/custom_levels_info.json", "read");
        custom_levels_info = JsonHelper.FromJson<levels_info>(jsonString);
    }

    //SAVE PLAYER DATA\\
    public static void SavePlayerData() //redo this function to incoperate andriod devices
    {
        string path = GetJsonDataByPlatform("/MenuJson/player_info.json", "read");
        FileStream file = File.Open(path, FileMode.Open);
        file.Close();
        string json_data = JsonUtility.ToJson(Instance.player_data, true);
        File.WriteAllText(path, json_data);
    }

    //GENERIC TEXTBOX CREATION\\
    public GameObject generictextbox;
    public Sprite[] textboxsymbols;
    public Transform canvas;
    
    public void TextBox(int index, string title, string description)
    {
        EditorManager.pillediting = false;
        GameObject current = Instantiate(generictextbox, new Vector2(Screen.width/2, Screen.height/2), Quaternion.identity, canvas);
        current.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = textboxsymbols[index];
        current.transform.GetChild(2).GetComponent<Text>().text = title;
        current.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = description;
    }

    //LOAD GAME SCENE\\
    public void PlayOnClick(string level)
    {
        if (level == "")
            level = LevelSelectManager.currently_selected.levelname;

        PlayerPrefs.SetString("levelname", level);
        SceneManager.LoadScene("Game");
    }

    //YES NO TEXTBOX CREATION\\
    //public GameObject yesnotextbox;
    //public static bool clickedyes;

    //public bool YesNoTextBox(string title, string description)
    //{
    //    clickedyes = false; //this doesnt get reset till I spawn another yesnobox
    //    EditorManager.pillediting = false;
    //    GameObject current = Instantiate(yesnotextbox, new Vector2(Screen.width / 2, Screen.height / 2), Quaternion.identity, canvas);
    //    current.transform.GetChild(0).GetComponent<Text>().text = title;
    //    current.transform.GetChild(1).GetComponent<Text>().text = description;

    //    return true;
    //}

    //QUIT GAME\\
    public void QuitGame()
    {
    #if UNITY_EDITOR || UNITY_STANDALONE
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    #if UNITY_ANDROID
        Application.Quit();
    #endif
    }

    //WAIT FOR WWW REQUEST\\
    public static IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

[System.Serializable]
public class levels_info
{
    public string levelname;
    public int pillamount;
    public int startinghealth;
    public int rewardpillindex1; // I wish Unity would fucking support json arrays already!
    public int rewardpillindex2;
    public int rewardpillindex3;
    public int rewardpillindex4;
}

[System.Serializable]
public class pill_data
{
    public float x;
    public float y;
    public int index; //sprite index
}

[System.Serializable]
public class player_info
{
    public int progress; //overall player progress
    public float masterslider;
    public float musicslider;
    public float fxslider;
    public string movementtype;
    public bool tooltips;
    public bool editorback;
}