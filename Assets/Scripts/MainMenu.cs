using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;
public class MainMenu : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    [SerializeField] Text[] keyInformation;
    [SerializeField] ControlsOptions controlsOptions;
    [SerializeField] GameObject[] menu;

    public static MainMenu sharedInstance;
    
    private GameObject currentKey;

    


    private Color32 normal = new Color32(255, 255, 255, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);

    private void Start()
    {
        sharedInstance = this;

        if (!FindObjectOfType<PlayerManager>().enabled) Cursor.lockState = CursorLockMode.None;

        ResetAllKeys();

        GameManager.OnGameReset += ResetGame; 

        keyInformation[0].text = keys["MoveForward"].ToString();
        keyInformation[1].text = keys["MoveBack"].ToString();
        keyInformation[2].text = keys["MoveLeft"].ToString();
        keyInformation[3].text = keys["MoveRight"].ToString();
        keyInformation[4].text = keys["Flashlight"].ToString();

    }


    public void ChangeMenu(GameObject newMenu)
    {
        foreach (GameObject currentMenu in menu )
        {
            currentMenu.SetActive(false);
        }
        newMenu.SetActive(true);
    }


    private void ResetAllKeys()
    {
        keys.Clear();
        keys.Add("MoveForward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveForward", "W")));
        keys.Add("MoveBack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveBack", "S")));
        keys.Add("MoveLeft", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeft", "A")));
        keys.Add("MoveRight", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRight", "D")));
        keys.Add("Flashlight", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Flashlight", "F")));

        controlsOptions.MoveForward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveForward"));
        controlsOptions.MoveBack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveBack"));
        controlsOptions.MoveLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeft"));
        controlsOptions.MoveRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRight"));
        controlsOptions.Flashlight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Flashlight"));
    }


    private void ResetGame()
    {
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
    }


    private void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.GetComponentInChildren<Text>().text = e.keyCode.ToString();
                Debug.Log(keys[currentKey.name] + "  " + e.keyCode);
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {

            PlayerPrefs.SetString(key.Key, key.Value.ToString());
            Debug.Log(PlayerPrefs.GetString(key.Key, key.Value.ToString()) + " saved");
        }

        PlayerPrefs.Save();
        ResetAllKeys();
    }

    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }


    public void QuitGame()
    {

        EditorApplication.ExitPlaymode();
        //EditorApplication.Exit(0);
        Application.Quit();
    }


    public void NewGame()
    {
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
    }


}
