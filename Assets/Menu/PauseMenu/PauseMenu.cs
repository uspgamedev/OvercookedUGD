using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;


public class PauseMenu : MonoBehaviour
{
private bool paused;

public GameObject PausePanel;
public GameObject OptionsMenu;
public string Scene;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
    }

    void PauseScreen()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            OptionsMenu.SetActive(false);
        }
        else
        {
            paused = true;
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
        }
    }

    public void BackMenu ()
    {
        SceneManager.LoadScene (Scene);
        Time.timeScale = 1f;
    }

    public void QuitGame ()
    {
        Debug.Log ("se n der erro foi milagre");
        Application.Quit();
    }

    public void Options(){
        OptionsMenu.SetActive(true);
        PausePanel.SetActive(false);
    }
}
