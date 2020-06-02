using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OptionKeyboard : MonoBehaviour
{
    public Text option1;
    public Text option2;
    public Text option3;
    public Text option4;

    private int numberOfOptions = 4;

    private int selectedOption;
    private bool once_check;
    public GameObject panel;
    private bool mute_on;
    private bool fullscreen_on;
    // Use this for initialization
    void Start()
    {
        selectedOption = 1;
        option1.color = new Color32(224, 221, 0, 255);
        option2.color = new Color32(255, 255, 255, 255);
        option3.color = new Color32(255, 255, 255, 255);
        option4.color = new Color32(255, 255, 255, 255);

        once_check = false;
        if (AudioListener.volume == 0f)
            mute_on = true;
        else mute_on = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioListener.volume == 0f)
            option3.text = "Mute On";
        else option3.text = "Mute Off";

        if (Screen.fullScreen)
            option2.text = "FullScreen On";
        else option2.text = "FullScreen Off";

        if (!panel.active)
        {
            if (Input.GetAxis("JoyVertical") < 0)
            {
                if (!once_check)
                {
                    selectedOption += 1;
                    if (selectedOption > numberOfOptions) //If at end of list go back to top
                    {
                        selectedOption = 1;
                    }

                    option1.color = new Color32(255, 255, 255, 255);
                    option2.color = new Color32(255, 255, 255, 255);
                    option3.color = new Color32(255, 255, 255, 255);
                    option4.color = new Color32(255, 255, 255, 255);

                    switch (selectedOption)
                    {
                        case 1:
                            option1.color = new Color32(224, 221, 0, 255);
                            break;
                        case 2:
                            option2.color = new Color32(224, 221, 0, 255);
                            break;
                        case 3:
                            option3.color = new Color32(224, 221, 0, 255);
                            break;
                        case 4:
                            option4.color = new Color32(224, 221, 0, 255);
                            break;
                    }
                }
                once_check = true;
            }
            else if (Input.GetAxis("JoyVertical") > 0)
            {
                if (!once_check)
                {
                    selectedOption -= 1;
                    if (selectedOption < 1)
                    {
                        selectedOption = numberOfOptions;
                    }

                    option1.color = new Color32(255, 255, 255, 255);
                    option2.color = new Color32(255, 255, 255, 255);
                    option3.color = new Color32(255, 255, 255, 255);
                    option4.color = new Color32(255, 255, 255, 255);

                    switch (selectedOption)
                    {
                        case 1:
                            option1.color = new Color32(224, 221, 0, 255);
                            break;
                        case 2:
                            option2.color = new Color32(224, 221, 0, 255);
                            break;
                        case 3:
                            option3.color = new Color32(224, 221, 0, 255);
                            break;
                        case 4:
                            option4.color = new Color32(224, 221, 0, 255);
                            break;
                    }
                }
                once_check = true;
            }
            else once_check = false;
            if (Input.GetKeyDown(KeyCode.DownArrow)/*|| Controller input*/)
            { //Input telling it to go up or down.
                selectedOption += 1;
                if (selectedOption > numberOfOptions) //If at end of list go back to top
                {
                    selectedOption = 1;
                }

                option1.color = new Color32(255, 255, 255, 255);
                option2.color = new Color32(255, 255, 255, 255);
                option3.color = new Color32(255, 255, 255, 255);
                option4.color = new Color32(255, 255, 255, 255);

                switch (selectedOption)
                {
                    case 1:
                        option1.color = new Color32(224, 221, 0, 255);
                        break;
                    case 2:
                        option2.color = new Color32(224, 221, 0, 255);
                        break;
                    case 3:
                        option3.color = new Color32(224, 221, 0, 255);
                        break;
                    case 4:
                        option4.color = new Color32(224, 221, 0, 255);
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)/*|| Controller input*/)
            {
                selectedOption -= 1;
                if (selectedOption < 1)
                {
                    selectedOption = numberOfOptions;
                }

                option1.color = new Color32(255, 255, 255, 255);
                option2.color = new Color32(255, 255, 255, 255);
                option3.color = new Color32(255, 255, 255, 255);
                option4.color = new Color32(255, 255, 255, 255);

                switch (selectedOption)
                {
                    case 1:
                        option1.color = new Color32(224, 221, 0, 255);
                        break;
                    case 2:
                        option2.color = new Color32(224, 221, 0, 255);
                        break;
                    case 3:
                        option3.color = new Color32(224, 221, 0, 255);
                        break;
                    case 4:
                        option4.color = new Color32(224, 221, 0, 255);
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
            {
                switch (selectedOption) //Set the visual indicator for which option you are on.
                {
                    case 1:
                        panel.SetActive(true);
                        //Resolution
                        break;
                    case 2:
                        if(!fullscreen_on)
                        {
                            option2.text = "FullScreen On";
                            Screen.SetResolution(Screen.width, Screen.height, true);
                            fullscreen_on = true;
                        }
                        else if(fullscreen_on)
                        {
                            option2.text = "FullScreen Off";
                            Screen.SetResolution(Screen.width, Screen.height, false);
                            fullscreen_on = false;
                        }
                        break;
                    case 3:
                        //Mute
                        if (!mute_on)
                        {
                            //option2.text = "Mute On";
                            AudioListener.volume = 0f;
                            mute_on = true;
                        }
                        else if (mute_on)
                        {
                            //option2.text = "Mute Off";
                            AudioListener.volume = 1f;
                            mute_on = false;
                        }
                        break;
                    case 4:
                        SceneManager.LoadScene(1); //Main Menu
                        break;
                }
            }
        }
    }
}
