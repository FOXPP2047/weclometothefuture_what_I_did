using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuKeyboard : MonoBehaviour
{
    public Text option1;
    public Text option2;
    public Text option3;
    public Text option4;
    public AudioSource[] audios;


    private int numberOfOptions = 4;

    private int selectedOption;
    private bool once_check;
    // Use this for initialization
    void Start()
    {
        selectedOption = 1;
        option1.color = new Color32(224, 221, 0, 255);
        option2.color = new Color32(255, 255, 255, 255);
        option3.color = new Color32(255, 255, 255, 255);
        option4.color = new Color32(255, 255, 255, 255);
        once_check = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("JoyVertical") < 0)
        {
            if (!once_check)
            {
                audios[0].Play();
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
                audios[0].Play();
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
            
            audios[0].Play();
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
            audios[0].Play();
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
            audios[1].Play();
            switch (selectedOption) //Set the visual indicator for which option you are on.
            {
                case 1:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    break;
                case 2:
                    SceneManager.LoadScene(6);
                    break;
                case 3:
                    SceneManager.LoadScene(5);
                    break;
                case 4:
                    Application.Quit();
                    break;
            }
        }
    }
}