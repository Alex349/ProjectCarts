using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public GameObject PauseScreen;
    public Image pauseImage;
    public GameObject m_HUD;
    public bool CanPause;

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
                m_HUD.SetActive(false);

                Debug.Log("pause");
                Time.timeScale = 0;
                CanPause = false;
                PauseScreen.SetActive(true);
                pauseImage.fillAmount = Screen.dpi;               

            }
            else
            {
                m_HUD.SetActive(true);

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
