using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    public GameObject characterGlow1, characterGlow2, characterGlow3, characterGlow4;

    void Start ()
    {
        characterGlow1.SetActive(false);
        characterGlow2.SetActive(false);
        characterGlow3.SetActive(false);
        characterGlow4.SetActive(false);
    }
	
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
            {
                if (hit.collider.name == "_Character1")
                    SelectedCharacter1();

                if (hit.collider.name == "_Character2")
                    SelectedCharacter2();

                if (hit.collider.name == "_Character3")
                    SelectedCharacter3();

                if (hit.collider.name == "_Character4")
                    SelectedCharacter4();
            }
        }
    }
    public void SelectedCharacter1()
    {
        Debug.Log("Character 1 SELECTED"); //Print out in the Unity console which character was selected.
        characterGlow1.SetActive(true);
        characterGlow2.SetActive(false);
        characterGlow3.SetActive(false);
        characterGlow4.SetActive(false);
    }

    public void SelectedCharacter2()
    {
        Debug.Log("Character 2 SELECTED");
        characterGlow1.SetActive(false);
        characterGlow2.SetActive(true);
        characterGlow3.SetActive(false);
        characterGlow4.SetActive(false);
    }

    public void SelectedCharacter3()
    {
        Debug.Log("Character 3 SELECTED");
        characterGlow1.SetActive(false);
        characterGlow2.SetActive(false);
        characterGlow3.SetActive(true);
        characterGlow4.SetActive(false);
    }

    public void SelectedCharacter4()
    {
        Debug.Log("Character 4 SELECTED");
        characterGlow1.SetActive(false);
        characterGlow2.SetActive(false);
        characterGlow3.SetActive(false);
        characterGlow4.SetActive(true);
    }
}
