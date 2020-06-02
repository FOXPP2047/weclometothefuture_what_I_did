using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameKeyboard : MonoBehaviour
{
    public Text option1;
    public Text option2;
    public Text option3;
    public AudioSource[] audios;

    private int max_num = 3;

    private int selectedOption;
    private bool once_check;
    // Use this for initialization
    void Start()
    {
        selectedOption = 1;
        option1.color = new Color32(224, 221, 0, 255);
        option2.color = new Color32(255, 255, 255, 255);
        option3.color = new Color32(255, 255, 255, 255);
        once_check = false;
    }

    void Awake()
    {
        selectedOption = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("JoyVertical") < 0)
        {
            audios[0].Play();
            if (!once_check)
            {
                selectedOption += 1;
                if (selectedOption > max_num) //If at end of list go back to top
                {
                    selectedOption = 1;
                }

                option1.color = new Color32(255, 255, 255, 255);
                option2.color = new Color32(255, 255, 255, 255);
                option3.color = new Color32(255, 255, 255, 255);

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
                }
            }
            once_check = true;
        }
        else if (Input.GetAxis("JoyVertical") > 0)
        {
            audios[0].Play();
            if (!once_check)
            {
                selectedOption -= 1;
                if (selectedOption < 1)
                {
                    selectedOption = max_num;
                }

                option1.color = new Color32(255, 255, 255, 255);
                option2.color = new Color32(255, 255, 255, 255);
                option3.color = new Color32(255, 255, 255, 255);

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
                }
            }
            once_check = true;
        }
        else once_check = false;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            audios[0].Play();
            selectedOption += 1;
            if (selectedOption > max_num)
            {
                selectedOption = 1;
            }

            option1.color = new Color32(255, 255, 255, 255); 
            option2.color = new Color32(255, 255, 255, 255);
            option3.color = new Color32(255, 255, 255, 255);

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
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            audios[0].Play();
            selectedOption -= 1;
            if (selectedOption < 1) 
            {
                selectedOption = max_num;
            }

            option1.color = new Color32(255, 255, 255, 255);
            option2.color = new Color32(255, 255, 255, 255);
            option3.color = new Color32(255, 255, 255, 255);

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
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
        {
            audios[1].Play();
            switch (selectedOption)
            {
                case 1:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    Time.timeScale = 1;
                    break;
                case 2:
                    SceneManager.LoadScene(1);
                    Time.timeScale = 1;
                    break;
                case 3:
                    Application.Quit();
                    break;
                default:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    Time.timeScale = 1;
                    break;
            }
        }
    }
}