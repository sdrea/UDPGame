using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class runGame : MonoBehaviour {

    static InputField inp;

    private void Start()
    {
        Screen.SetResolution(800, 600, false);
        inp = GameObject.Find("InputField").GetComponent<InputField>();
        inp.gameObject.SetActive(true);
        inp.ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            loadGame();
        }
    }

    public void loadGame ()
    {
        inp = GameObject.Find("InputField").GetComponent<InputField>();
        
        if (inp.text == "")
        {
            PlayerPrefs.SetString("Name", "Bitties");
        }
        else
        {
            PlayerPrefs.SetString("Name", inp.text);
        }
        SceneManager.LoadScene(1);
        
        
    }
}
