using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public GameObject PauseScreen;
    public Image pauseImage;
    public GameObject[] m_HUD;
    public bool CanPause;

    void Start ()
    {
        pauseImage = PauseScreen.GetComponentInChildren<Image>();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (CanPause)
            {
                for (int i = 0; i < m_HUD.Length; i++)
                {
                    m_HUD[i].SetActive(false);
                }
                Debug.Log("pause");
                Time.timeScale = 0;
                CanPause = false;
                PauseScreen.SetActive(true);
                pauseImage.fillAmount = Screen.width;               

            }
            else
            {
                for (int i = 0; i < m_HUD.Length; i++)
                {
                    m_HUD[i].SetActive(true);
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
