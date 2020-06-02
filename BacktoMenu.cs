using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BacktoMenu : MonoBehaviour
{
    public Text option1;

    private int selectedOption;

    // Use this for initialization
    void Start()
    {
        selectedOption = 1;
        option1.color = new Color32(224, 221, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
        {
            switch (selectedOption)
            {
                case 1:
                    option1.color = new Color32(224, 221, 0, 255);
                    SceneManager.LoadScene(1);
                    break;
            }
        }
    }
}
