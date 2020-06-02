using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Image fade1;
    public Image fade2;

    private Color tempColor;

    public Image team_logo;
    private Image digipen_logo;

    public Text team_text;

    public float duration;

    private float timer;
    private float timer_stop;
    private float timer_last;
    
    private bool is_team_logo;
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        timer = 0f;
        timer_stop = 0f;
        timer_last = 0f;
        digipen_logo = this.GetComponent<Image>();

        tempColor = fade1.GetComponent<Image>().color;
        is_team_logo = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (fade1.GetComponent<Image>().color.a < 1f && !is_team_logo)
        {
            if (timer < duration / 2)
            {
                digipen_logo.enabled = true;
                team_logo.enabled = false;
                team_text.enabled = false;
            }

            else
            {
                tempColor.a += 0.015f;
                fade1.GetComponent<Image>().color = tempColor;
            }
        }

        else if (fade1.GetComponent<Image>().color.a > 1f && !is_team_logo)
        {
            timer = 0f;
            is_team_logo = true;
            tempColor.a = 0f;
        }

        else if (is_team_logo && timer_stop < 0.3f)
        {
            timer_stop += Time.deltaTime;
        }

        else if (is_team_logo && timer_stop > 0.3f)
        {
            if (timer < duration / 2)
            {
                digipen_logo.enabled = false;
                team_logo.enabled = true;
                team_text.enabled = true;
            }

            else if (timer > duration / 2 && fade2.GetComponent<Image>().color.a < 1f)
            {
                tempColor.a += 0.015f;
                fade2.GetComponent<Image>().color = tempColor;
            }

            else if (fade2.GetComponent<Image>().color.a > 1f && timer_last < 0.3f)
            {
                timer_last += Time.deltaTime;
            }

            else if(fade2.GetComponent<Image>().color.a > 1f && timer_last > 0.3f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
