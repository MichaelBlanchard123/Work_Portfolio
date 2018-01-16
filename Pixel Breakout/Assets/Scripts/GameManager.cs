using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //MULTI FUNCTION VARIABLES\\
    public static GameManager Instance;
    public static int lives = 3;
    public static bool hasBeenLaunched;
    public int pillsremaining = 0;

    void Awake()
    {
        Instance = this;
    }

    //START\\
    void Start()
    {
        SetupLevel();
        ChangeLifeAmount();
        resume.onClick.AddListener(TogglePause);
        InvokeRepeating("UpdateStatusEffects", 0, 1);
        InvokeRepeating("PillsRemaining", 0, 1);
    }

    //PAUSED USING ANDROID BACK KEY OR ESCAPE KEY\\
    public Button resume;
    public GameObject pausedmenu;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            TogglePause();
        }
    }

    //PILL REMAINING CALCULATION\\ 
    private void PillsRemaining() //+++ LAG +++ change they way pills are calculated if to demanding
    {
        pillsremaining = 0;

        foreach (Transform child in pillsparent.transform)
        {
            if (child.tag == "Pill")
                if(child.name != "Steel Pill(Clone)")
                    pillsremaining++;
        }
    }

    //STATUS EFFECTS\\
    public GameObject[] effectslots;
    public Sprite[] pillthumbs;

    void UpdateStatusEffects()
    {
        int slots = 0;
        int lenght = 0;

        foreach (int effect in AppManager.Instance.statuseffects) //loop all effects
        {
            if(effect > 0) // if effect is active
            {
                if(slots <= 4) // loop all available slots
                {
                    if (!effectslots[slots].activeSelf) // find empty slot
                        effectslots[slots].SetActive(true);

                    effectslots[slots].transform.GetChild(0).gameObject.GetComponent<Text>().text = effect.ToString();
                    effectslots[slots].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = pillthumbs[lenght];
                }
                slots++;
            }
            lenght++;

            for(int f = slots; f < 4; f++)
            {
                effectslots[f].SetActive(false);
            }
        }
    }

    //PAUSE TOGGLE\\
    void TogglePause()
    {
        if (pausedmenu.activeSelf == false)
        {
            leveltext.text = "";

            pausedmenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (pausedmenu.activeSelf == true)
        {
            leveltext.text = PlayerPrefs.GetString("levelname");

            pausedmenu.SetActive(false);
            Time.timeScale = 0.1f;
            StartCoroutine("PauseScreenDelay");
        }
    }

    //PAUSED MENU\\
    private IEnumerator PauseScreenDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1;
    }

    //LOSE LIFE\\
    public void LoseLife()
    {
        lives--;
        ChangeLifeAmount();
        Destroy(cpaddle);
        Invoke("SetupPaddle", 1f);
        hasBeenLaunched = false;

        for (int x = 0; x < AppManager.Instance.statuseffects.Length; x++) //reset effects
        {
            if(AppManager.Instance.statuseffects[x] > 1)
                AppManager.Instance.statuseffects[x] = 0;
        }
        //CheckGameOver();  //make this function.
    }

    //BACK TO MAINMENU\\
    public void BackToMainMenu(bool j)
    {
        if(j)
            AppManager.Instance.player_data.editorback = true;

        Time.timeScale = 1; //reset timescale to 1
        AppManager.SavePlayerData();
        SceneManager.LoadScene("Main Menu");
    }

    //FROM JSON SETUP\\
    public Text leveltext;
    public Transform pillsparent;

    public GameObject paddle;
    public GameObject cpaddle;

    public void SetupLevel()
    {
        pill_data[] pill_data;
        string levelname = PlayerPrefs.GetString("levelname");

        leveltext.text = levelname;

        if (levelname == "preview")
            pausedmenu.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
        else
            pausedmenu.transform.GetChild(1).GetChild(0).GetChild(3).gameObject.SetActive(false);

        string jsonString = AppManager.GetJsonDataByPlatform("/Json/" + levelname + "_Data.json", "read");
        pill_data = JsonHelper.FromJson<pill_data>(jsonString);

        for (int i = 0; i < 320; i++)
        {
            GameObject current = Instantiate(AppManager.Instance.pilltypes[pill_data[i].index / 7], new Vector2(pill_data[i].x, pill_data[i].y + 1.44f), Quaternion.identity, pillsparent);
            current.GetComponent<SpriteRenderer>().sprite = AppManager.Instance.pilltypeandcolor[pill_data[i].index];
        }

        cpaddle = Instantiate(paddle, new Vector2(transform.position.x, -4f), Quaternion.identity) as GameObject;
        hasBeenLaunched = false;
    }

    //SPAWN PADDLE
    void SetupPaddle()
    {
        cpaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
    }

    //HEART HANDLING\\
    public GameObject[] Hearts;
    public Sprite HeartFull;
    public Sprite HeartEmpty;
    public Text ExtraHeartsText;

    public static void ChangeLifeAmount()
    {
        if (lives <= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                if (lives > i)
                    Instance.Hearts[i].GetComponent<SpriteRenderer>().sprite = Instance.HeartFull;
                else
                    Instance.Hearts[i].GetComponent<SpriteRenderer>().sprite = Instance.HeartEmpty;
            }
            Instance.ExtraHeartsText.text = "";
        }
        else
            Instance.ExtraHeartsText.text = "+" + (lives - 5).ToString();
    }
}