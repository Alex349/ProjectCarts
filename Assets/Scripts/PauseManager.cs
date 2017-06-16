using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public GameObject PauseScreen;
    public GameObject m_HUD;
    public bool CanPause;
    public GameObject[] hudComponents;

    void Start ()
    {
        CanPause = true;        
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P) ||Input.GetButtonDown("Pause"))
        {
            if (CanPause)
            {
                for (int i = 0; i < hudComponents.Length; i++)
                {                    
                    hudComponents[i].SetActive(false);
                }                

                Debug.Log("pause");
                Time.timeScale = 0;
                audioManager.audioInstance.StopAllSounds();

                PauseScreen.SetActive(true);
                CanPause = false;
            }
            else
            {
                for (int i = 0; i < hudComponents.Length; i++)
                {
                    hudComponents[i].SetActive(true);
                }

                Debug.Log("Running");
                Time.timeScale = 1;
                CanPause = true;
                PauseScreen.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && CanPause == false)
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}
