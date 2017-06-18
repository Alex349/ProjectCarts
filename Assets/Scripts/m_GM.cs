using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class m_GM : MonoBehaviour
{
    public static m_GM gameManager;
    private GameObject[] mainPlayer;
    private GameObject[] IAPlayers;
    //public GameObject[] initSpawnPoints;
    public static GameObject CanvasAmazing, LapCheckPoints, AlertBoxHUD, OffScreenLogic;

    private static Camera m_camera;

    public Animator cameraAnimator;
    private static Animation cameraAnimation;
    public static float animDuration = 1;
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

        mainPlayer = GameObject.FindGameObjectsWithTag("Player");
        IAPlayers = GameObject.FindGameObjectsWithTag("Kart");

        for (int i = 0; i < mainPlayer.Length; i++)
        {          
            mainPlayer[i].SetActive(false);
        }
        for (int i = 0; i < IAPlayers.Length; i++)
        {
            IAPlayers[i].SetActive(false);
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
        
    }
	void Start ()
    {
        

    }
	
	void Update ()
    {
        if (SceneManager.GetActiveScene().name == "Gold_Version_Eduard")
        {
            animDuration -= Time.deltaTime;

            //l'animació són uns 17s
            if (animDuration <= 0 && !cameraAnimator.GetBool("raceStart") || Input.GetKey("enter"))
            {
                audioManager.audioInstance.StopAllSounds();               

                for (int i = 0; i <= mainPlayer.Length; i++)
                {
                    if (MenuScript.SelectionIndex == i)
                    {
                        CanvasAmazing.SetActive(true);

                        if (i == 0)
                        {
                            mainPlayer[i].SetActive(true);
                            mainPlayer[i] = GameObject.Find("PlayerKart_Char1");
                        }
                        else if (i == 1)
                        {
                            mainPlayer[i].SetActive(true);
                            mainPlayer[i] = GameObject.Find("PlayerKart_Char2");
                        }
                        else if (i == 2)
                        {
                            mainPlayer[i].SetActive(true);
                            mainPlayer[i] = GameObject.Find("PlayerKart_Char3");
                        }
                        else if (i == 3)
                        {
                            mainPlayer[i].SetActive(true);
                            mainPlayer[i] = GameObject.Find("PlayerKart_Char4");
                        }                                                

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
                audioManager.audioInstance.PauseCinematicMusic();
            }
            else
            {
                audioManager.audioInstance.CinematicMusic();
            }
        }      
    }    
}
