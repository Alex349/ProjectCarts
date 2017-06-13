using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_GM : MonoBehaviour
{
    public static m_GM gameManager;
    public GameObject[] mainPlayer;
    public GameObject[] IAPlayers;
    public GameObject[] initSpawnPoints;
    public static GameObject CanvasAmazing, LapCheckPoints, AlertBoxHUD, OffScreenLogic;

    private static Camera m_camera;

    public Animator cameraAnimator;
    private static Animation cameraAnimation;
    public static float animDuration;
    public static bool managerReady;
    private int totalIA = 0, totalPlayers = 0;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameManager);
        }

        managerReady = false;
        CanvasAmazing = GameObject.Find("Canvas_Gold");
        CanvasAmazing.SetActive(false);
        LapCheckPoints = GameObject.Find("LapCheckPoints");
        LapCheckPoints.SetActive(false);
        AlertBoxHUD = GameObject.Find("AlertBoxHUD");
        AlertBoxHUD.SetActive(false);
        OffScreenLogic = GameObject.Find("OffScreenLogic");
        OffScreenLogic.SetActive(false);

        m_camera = Camera.main;        
        m_camera.GetComponent<CameraScript>().enabled = false;

        cameraAnimator = Camera.main.GetComponent<Animator>();
        cameraAnimator.SetBool("raceStart", false);  
        
        for (int i = 0; i < mainPlayer.Length; i++)
        {
            mainPlayer[i].SetActive(false);
        }      
        for (int i = 0; i < IAPlayers.Length; i++)
        {
            IAPlayers[i].SetActive(false);
        }
    }
	void Start ()
    {
        

    }
	
	void Update ()
    {
        animDuration += Time.deltaTime;

        if (animDuration >= 1 && !cameraAnimator.GetBool("raceStart"))
        {
            audioManager.audioInstance.StopAllSounds();

            for (int i = 0; i < mainPlayer.Length; i++)
            {
                if (MenuScript.SelectionIndex == i)
                {
                    CanvasAmazing.SetActive(true);

                    mainPlayer[i].SetActive(true);
                    OffScreenLogic.SetActive(true);
                    AlertBoxHUD.SetActive(true);
                    LapCheckPoints.SetActive(true);
                    totalPlayers++;
                }
            }
            for (int r = 0; r < IAPlayers.Length; r++)
            {
                IAPlayers[r].SetActive(true);
                totalIA++;
            }

            managerReady = true;
            cameraAnimator.SetBool("raceStart", true);
            cameraAnimator.enabled = false;
            m_camera.GetComponent<CameraScript>().enabled = true;

        }
        else
        {
            audioManager.audioInstance.CinematicMusic();
        }        
    }    
}
