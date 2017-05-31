using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu, mainPauseMenu;
    public Slider volumeSlider;
    public Toggle[] TogglesOfResolution;
    public Button[] mainPauseMenuButtons;
    public int[] screenRectWidth;
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
    void Update()
    {
        if (mainPauseMenu != null)
        {
            mainPauseMenuButtons[0].Select();

            if (Input.GetAxis("Vertical") < 0)
            {
                if (mainPauseMenuButtons[0].isActiveAndEnabled)
                {
                    mainPauseMenuButtons[1].Select();
                }
                else if (mainPauseMenuButtons[1].isActiveAndEnabled)
                {
                    mainPauseMenuButtons[2].Select();
                }
                else if (mainPauseMenuButtons[2].isActiveAndEnabled)
                {
                    mainPauseMenuButtons[0].Select();
                }
            }            
            else if (Input.GetAxis("Vertical") > 0)
            {
                if (mainPauseMenuButtons[0].isActiveAndEnabled)
                {
                    mainPauseMenuButtons[2].Select();
                }
                else if (mainPauseMenuButtons[1].isActiveAndEnabled)
                {
                    mainPauseMenuButtons[0].Select();
                }
                else if (mainPauseMenuButtons[2].isActiveAndEnabled)
                {
                    mainPauseMenuButtons[1].Select();
                }
            }
        }
        else if (pauseMenu != null)
        {
            TogglesOfResolution[0].Select();

            if (Input.GetAxis("Vertical") < 0)
            {
                if (TogglesOfResolution[0].isActiveAndEnabled)
                {
                    TogglesOfResolution[1].Select();
                }
                else if (TogglesOfResolution[1].isActiveAndEnabled)
                {
                    TogglesOfResolution[2].Select();
                }
                else if (TogglesOfResolution[2].isActiveAndEnabled)
                {
                    TogglesOfResolution[3].Select();
                }
                else if (TogglesOfResolution[3].isActiveAndEnabled)
                {
                    TogglesOfResolution[0].Select();
                }
            }           

            else if (Input.GetAxis("Vertical") > 0)
            {
                if (TogglesOfResolution[0].isActiveAndEnabled)
                {
                    TogglesOfResolution[3].Select();
                }
                else if (TogglesOfResolution[1].isActiveAndEnabled)
                {
                    TogglesOfResolution[0].Select();
                }
                else if (TogglesOfResolution[2].isActiveAndEnabled)
                {
                    TogglesOfResolution[3].Select();
                }
                else if (TogglesOfResolution[3].isActiveAndEnabled)
                {
                    TogglesOfResolution[0].Select();
                }
            }            
        }       
        
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        
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
