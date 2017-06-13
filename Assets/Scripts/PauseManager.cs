using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public GameObject PauseScreen;
    public Image pauseImage;
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
                    hudComponents[0].SetActive(true);
                }                

                Debug.Log("pause");
                Time.timeScale = 0;
                
                PauseScreen.SetActive(true);
                pauseImage.fillAmount = Screen.dpi;
                CanPause = false;
            }
            else
            {
                for (int i = 0; i < hudComponents.Length; i++)
                {
                    hudComponents[i].SetActive(true);
                    hudComponents[0].SetActive(false);
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
