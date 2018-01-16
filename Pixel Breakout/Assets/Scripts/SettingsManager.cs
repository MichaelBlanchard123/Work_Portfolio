using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour {

    //START\\
    public Slider masterslider;
    public Slider musicslider;
    public Slider fxslider;

    void Start()
    {
        masterslider.value = AppManager.Instance.player_data.masterslider;
        musicslider.value = AppManager.Instance.player_data.musicslider;
        fxslider.value = AppManager.Instance.player_data.fxslider;

        ToggleTooltipSprites();
    }

    //TOOLTIP TOGGLE\\
    public void ToggleTooltips()
    {
        if (AppManager.Instance.player_data.tooltips == false)
            AppManager.Instance.player_data.tooltips = true;
        else
            AppManager.Instance.player_data.tooltips = false;

        ToggleTooltipSprites();
    }

    //TOOLTIP SPRITE\\
    public GameObject tooltipbutton;
    public Sprite redmenu;
    public Sprite greenmenu;

    public void ToggleTooltipSprites()
    {
        if (AppManager.Instance.currentparent.name == "Menu Pills")
        {
            if (AppManager.Instance.player_data.tooltips == true)
                tooltipbutton.GetComponent<Image>().sprite = greenmenu;
            else
                tooltipbutton.GetComponent<Image>().sprite = redmenu;
        }
    }

    //DELETE PLAYER PROGRESS\\
    public void DeletePlayerProgess()
    {
        // ADD YES/NO TEXTBOX HERE
        AppManager.Instance.player_data.progress = 1;
    }

    //DELETE ALL CUSTOM LEVELS\\
    public void DeleteAllLevels()
    {
        // ADD YES/NO TEXTBOX HERE
        foreach (levels_info level in AppManager.custom_levels_info)
        {
            string paththumb = Application.streamingAssetsPath + "/Thumbnail/" + level.levelname + "_thumb.png";
            string pathjson = Application.streamingAssetsPath + "/Json/" + level.levelname + "_Data.json";

            File.Delete(paththumb);
            File.Delete(pathjson);
        }
        string pathcustomjson = Application.streamingAssetsPath + "/custom_levels_info.json";
        string jsonString = File.ReadAllText(pathcustomjson);

        jsonString = jsonString.Remove(10, jsonString.IndexOf("]") - 10);

        File.WriteAllText(pathcustomjson, jsonString);
    }

    //SLIDER VOLUME\\
    public AudioMixer audiomixer;

    public void SetMasterLvl(float masterLvl) //+++ LAG +++ only set audiomixer.setfloat once onDisable if too much lag occurs
    {
        audiomixer.SetFloat("MasterVol", masterLvl);
        AppManager.Instance.player_data.masterslider = masterLvl;
    }

    public void SetMusicLvl(float musicLvl)
    {
        audiomixer.SetFloat("MusicVol", musicLvl);
        AppManager.Instance.player_data.musicslider = musicLvl;
    }

    public void SetFxLvl(float fxLvl)
    {
        audiomixer.SetFloat("FxVol", fxLvl);
        AppManager.Instance.player_data.fxslider = fxLvl;
    }

    //SAVING SETTINGS TO JSON
    void OnDisable()
    {
        string path = Application.streamingAssetsPath + "/player_info.json";

        FileStream file = File.Open(path, FileMode.Open);
        file.Close();

        string json_data = JsonUtility.ToJson(AppManager.Instance.player_data, true);
        File.WriteAllText(path, json_data);
    }
}