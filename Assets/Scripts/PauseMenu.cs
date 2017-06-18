﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu, mainPauseMenu;
    public Slider volumeSlider;
    public Toggle[] TogglesOfResolution;
    public Button[] mainPauseMenuButtons;
    public int[] screenRectWidth;
    int activeScreenResIndex;
    private PauseManager m_pauseManager;

    void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullScreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;        

        pauseMenu.SetActive(false);
        mainPauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        m_pauseManager = FindObjectOfType<PauseManager>();
    }
    void Update()
    {
        //if (mainPauseMenu != null)
        //{
        //    for (int i = 0; i < mainPauseMenuButtons.Length; i++)
        //    {
        //        if (mainPauseMenuButtons[i].isActiveAndEnabled == false)
        //        {
        //            mainPauseMenuButtons[0].Select();
        //        }
        //    }
        //
        //    if (Input.GetAxis("Vertical") < 0)
        //    {
        //        if (mainPauseMenuButtons[0].isActiveAndEnabled)
        //        {
        //            mainPauseMenuButtons[1].Select();
        //        }
        //        else if (mainPauseMenuButtons[1].isActiveAndEnabled)
        //        {
        //            mainPauseMenuButtons[2].Select();
        //        }
        //        else if (mainPauseMenuButtons[2].isActiveAndEnabled)
        //        {
        //            mainPauseMenuButtons[0].Select();
        //        }
        //    }            
        //    else if (Input.GetAxis("Vertical") > 0)
        //    {
        //        if (mainPauseMenuButtons[0].isActiveAndEnabled)
        //        {
        //            mainPauseMenuButtons[2].Select();
        //        }
        //        else if (mainPauseMenuButtons[1].isActiveAndEnabled)
        //        {
        //            mainPauseMenuButtons[0].Select();
        //        }
        //        else if (mainPauseMenuButtons[2].isActiveAndEnabled)
        //        {
        //            mainPauseMenuButtons[1].Select();
        //        }
        //    }
        //}
        //else if (pauseMenu != null)
        //{            
        //    for (int i = 0; i < TogglesOfResolution.Length; i++)
        //    {
        //        if (TogglesOfResolution[i].isActiveAndEnabled == false)
        //        {
        //            TogglesOfResolution[0].Select();
        //        }
        //    }
        //    if (Input.GetAxis("Vertical") < 0)
        //    {
        //        if (TogglesOfResolution[0].isActiveAndEnabled)
        //        {
        //            TogglesOfResolution[1].Select();
        //        }
        //        else if (TogglesOfResolution[1].isActiveAndEnabled)
        //        {
        //            TogglesOfResolution[2].Select();
        //        }
        //        else if (TogglesOfResolution[2].isActiveAndEnabled)
        //        {
        //            TogglesOfResolution[3].Select();
        //        }
        //        else if (TogglesOfResolution[3].isActiveAndEnabled)
        //        {
        //            TogglesOfResolution[0].Select();
        //        }
        //    }           
        //
        //    else if (Input.GetAxis("Vertical") > 0)
        //    {
        //        if (TogglesOfResolution[0].isActiveAndEnabled)
        //        {
        //            TogglesOfResolution[3].Select();
        //        }
        //        else if (TogglesOfResolution[1].isActiveAndEnabled)
        //        {
        //            TogglesOfResolution[0].Select();
        //        }
        //        else if (TogglesOfResolution[2].isActiveAndEnabled)
        //        {
        //            TogglesOfResolution[3].Select();
        //        }
        //        else if (TogglesOfResolution[3].isActiveAndEnabled)
        //        {
        //            TogglesOfResolution[0].Select();
        //        }
        //    }            
        //}       
        
    }
    public void ResumeGame()
    {
        audioManager.audioInstance.ButtonMenuOK();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        for (int i = 0; i < m_pauseManager.hudComponents.Length; i++)
        {
            m_pauseManager.hudComponents[i].SetActive(true);
        }
        audioManager.audioInstance.PlayAllSounds();
    }
    public void OptionsMenu()
    {
        audioManager.audioInstance.ButtonMenuOK();
        mainPauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void MainPauseMenu()
    {
        audioManager.audioInstance.ButtonMenuOK();

        optionsMenu.SetActive(false);
        mainPauseMenu.SetActive(true);
    }
    public void Exit()
    {
        audioManager.audioInstance.ButtonMenuBack();

        Application.Quit();
    }
    public void Back()
    {
        audioManager.audioInstance.ButtonMenuBack();
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
            audioManager.audioInstance.ButtonMenuOK();
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
