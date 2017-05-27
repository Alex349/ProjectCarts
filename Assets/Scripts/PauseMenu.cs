﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu, mainPauseMenu;
    public Slider volumeSlider;
    public Toggle[] TogglesOfResolution;
    public int[] screenRectWidth;
    public PauseManager m_pauseMng;
    int activeScreenResIndex;
    private AudioListener cameraListener;

    void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullScreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;        
        cameraListener = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
        pauseMenu.SetActive(false);
        mainPauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        m_pauseMng.CanPause = true;
    }
    public void OptionsMenu()
    {
        mainPauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Back()
    {
        mainPauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    public void SetResolutionOfScreen(int i)
    {
        if (TogglesOfResolution[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9;
            Screen.SetResolution(screenRectWidth[i], (int)(screenRectWidth[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }

    }
    public void SetGlobalFullScreen(bool isFullGobalScreen)
    {
        for (int i = 0; i < TogglesOfResolution.Length; i++)
        {
            TogglesOfResolution[i].interactable = !isFullGobalScreen;

        }
        if (isFullGobalScreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetResolutionOfScreen(activeScreenResIndex);
        }
        PlayerPrefs.SetInt("fullscreen", ((isFullGobalScreen) ? 1 : 0));
        PlayerPrefs.Save();

    }
    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = volumeSlider.value;
    }
}
