using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LastLevel : MonoBehaviour
{
    public Text option1;
    public Text option2;
    public GameObject player;
    private PlayerController cc;
    private int numberOfOptions = 2;
    public AudioSource[] audios;

    private int selectedOption;
    private bool once_check;
    // Use this for initialization
    void Start()
    {
        cc = player.GetComponent<PlayerController>();
        selectedOption = 1;
        option1.color = new Color32(224, 221, 0, 255);
        option2.color = new Color32(255, 255, 255, 255);
        once_check = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.is_stage_ended)
        {
            option1.enabled = true;
            option2.enabled = true;
        }

        else
        {
            option1.enabled = false;
            option2.enabled = false;
        }

        if(option1.enabled && option2.enabled)
        {
            if (Input.GetAxis("JoyVertical") < 0)
            {
                if (cc.is_stage_ended)
                    audios[0].Play();

                if (!once_check)
                {
                    selectedOption += 1;
                    if (selectedOption > numberOfOptions) //If at end of list go back to top
                    {
                        selectedOption = 1;
                    }

                    option1.color = new Color32(255, 255, 255, 255);
                    option2.color = new Color32(255, 255, 255, 255);

                    switch (selectedOption)
                    {
                        case 1:
                            option1.color = new Color32(224, 221, 0, 255);
                            break;
                        case 2:
                            option2.color = new Color32(224, 221, 0, 255);
                            break;
                    }
                }
                once_check = true;
            }
            else if (Input.GetAxis("JoyVertical") > 0)
            {
                if (cc.is_stage_ended)
                    audios[0].Play();
                if (!once_check)
                {
                    selectedOption -= 1;
                    if (selectedOption < 1)
                    {
                        selectedOption = numberOfOptions;
                    }

                    option1.color = new Color32(255, 255, 255, 255);
                    option2.color = new Color32(255, 255, 255, 255);

                    switch (selectedOption)
                    {
                        case 1:
                            option1.color = new Color32(224, 221, 0, 255);
                            break;
                        case 2:
                            option2.color = new Color32(224, 221, 0, 255);
                            break;
                    }
                }
                once_check = true;
            }
            else once_check = false;

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (cc.is_stage_ended)
                    audios[0].Play();
                selectedOption += 1;
                if (selectedOption > numberOfOptions)
                {
                    selectedOption = 1;
                }

                option1.color = new Color32(255, 255, 255, 255);
                option2.color = new Color32(255, 255, 255, 255);

                switch (selectedOption)
                {
                    case 1:
                        option1.color = new Color32(224, 221, 0, 255);
                        break;
                    case 2:
                        option2.color = new Color32(224, 221, 0, 255);
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (cc.is_stage_ended)
                    audios[0].Play();
                selectedOption -= 1;
                if (selectedOption < 1)
                {
                    selectedOption = numberOfOptions;
                }

                option1.color = new Color32(255, 255, 255, 255);
                option2.color = new Color32(255, 255, 255, 255);

                switch (selectedOption)
                {
                    case 1:
                        option1.color = new Color32(224, 221, 0, 255);
                        break;
                    case 2:
                        option2.color = new Color32(224, 221, 0, 255);
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
            {
                if (cc.is_stage_ended)
                    audios[1].Play();
                switch (selectedOption)
                {
                    case 1:
                        option1.color = new Color32(224, 221, 0, 255);
                        SceneManager.LoadScene(1);
                        break;
                    case 2:
                        Application.Quit();
                        break;
                }
            }
        }

    }
}
