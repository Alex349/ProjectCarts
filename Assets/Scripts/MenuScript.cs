using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static MenuScript m_mainMenu;
    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;
    public GameObject ArcadeMenuHolder;
    public GameObject CharacterMenuHolder;
    public GameObject[] models;

    public Slider volumeSlider;
    public Toggle[] resolutionToggles;
    public int[] screenWidths;
    int activeScreenResIndex;
    public static int SelectionIndex = 0;

    void Awake()
    {
        if (m_mainMenu == null)
        {
            m_mainMenu = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullScreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;

        mainMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);
        ArcadeMenuHolder.SetActive(false);
        CharacterMenuHolder.SetActive(false);        
    }
    void Update()
    {
        
    }
    public void Play()
    {
        audioManager.audioInstance.ButtonMenuOK();
        SceneManager.LoadScene("Gold_Version"); 
               
    }
    public void ArcadeMenu()
    {
        audioManager.audioInstance.ButtonMenuNext();
        mainMenuHolder.SetActive(false);
        ArcadeMenuHolder.SetActive(true);
        CharacterMenuHolder.SetActive(false);
    }
    public void CharacterMenu()
    {
        audioManager.audioInstance.ButtonMenuNext();
        ArcadeMenuHolder.SetActive(false);
        CharacterMenuHolder.SetActive(true);
        models[SelectionIndex].SetActive(true);
    }
    public void Quit()
    {
        audioManager.audioInstance.ButtonMenuBack();
        Application.Quit();
    }
    public void OptionsMenu()
    {
        audioManager.audioInstance.ButtonMenuNext();
        mainMenuHolder.SetActive(false);
        optionsMenuHolder.SetActive(true);
        ArcadeMenuHolder.SetActive(false);
    }
    public void MainMenu()
    {
        audioManager.audioInstance.ButtonMenuBack();
        mainMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);
        ArcadeMenuHolder.SetActive(false);
    }
    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }
    public void SetFullScreen (bool isFullScreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullScreen;            
        }
        if (isFullScreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }
        PlayerPrefs.SetInt("fullscreen", ((isFullScreen) ? 1 : 0));
        PlayerPrefs.Save();

    }
	public void SetVolume(float value)
    {
        AudioListener.volume = volumeSlider.value;
    }   
    public void Select (int index)
    {
        if (index == SelectionIndex)
        {
            return;
        }
        if (index < 0 || index >= models.Length)
        {
            return;
        }
        models[SelectionIndex].SetActive(false);
        SelectionIndex = index;
        audioManager.audioInstance.ButtonMenuNext();
        models[SelectionIndex].SetActive(true);
    }
}
