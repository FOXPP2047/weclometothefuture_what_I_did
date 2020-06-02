using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TotalScore : MonoBehaviour
{
    private Text total_score_text;
    private TimerText tt;
    private TimerScore ts;
    private RemainUI rui;
    private bool is_text_ienumerator;
    private bool is_score_ienumerator;
    private bool is_end_typing;
    private string m_typing_text;
    private int score;
    private int score_text;
    // Start is called before the first frame update
    void Start()
    {
        total_score_text = this.GetComponent<Text>();
        ts = GameObject.Find("Timer").GetComponent<TimerScore>();
        rui = GameObject.Find("Heart_Score").GetComponent<RemainUI>();
        tt = GameObject.Find("Timer_Score").GetComponent<TimerText>();
        is_text_ienumerator = false;
        is_score_ienumerator = false;
        is_end_typing = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ts.GetComponent<TimerScore>().is_timer_score_ended)
        {
            total_score_text.enabled = false;
        }
        else
        {
            total_score_text.enabled = true;

            m_typing_text = ("Total Score : ");

            if (!is_text_ienumerator && !is_end_typing)
                StartCoroutine(typing_text());
            else if (is_end_typing)
            {
                if(tt.GetComponent<TimerText>().add_timer_integer <= 90)
                    score = rui.GetComponent<RemainUI>().add_heart_integer * (90 - tt.GetComponent<TimerText>().add_timer_integer);
                else score = rui.GetComponent<RemainUI>().add_heart_integer;
                score *= 10;

                if (!is_score_ienumerator)
                    StartCoroutine(typing_text_score());
                total_score_text.text = m_typing_text + score_text.ToString();
            }
        }
    }

    IEnumerator typing_text()
    {
        is_text_ienumerator = true;

        for (int i = 0; i < m_typing_text.Length; ++i)
        {
            total_score_text.text = m_typing_text.Substring(0, i);
            if (i == m_typing_text.Length - 1)
            {
                is_end_typing = true;
            }

            yield return new WaitForSeconds(0.05f);
        }
        is_text_ienumerator = false;
    }

    IEnumerator typing_text_score()
    {
        is_score_ienumerator = true;

        if(score_text < score)
        {
            if (score - score_text > 30)
            {
                score_text += 30;
                yield return new WaitForSeconds(0.001f);
            }
                
            else if(score - score_text > 5)
            {
                score_text++;
                yield return new WaitForSeconds(0.001f);
                
            }

            else if(score - score_text <= 5)
            {
                score_text++;
                yield return new WaitForSeconds(0.01f);
            }   
        }
        is_score_ienumerator = false;
    }
}
