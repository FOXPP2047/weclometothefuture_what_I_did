using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    [HideInInspector]
    public bool is_timer_text_start;

    private RemainUI rui;
    private Timer timer;

    private Text timer_score_text;
    private string m_typing_text;
    [HideInInspector]
    public bool is_end_typing;
    private bool is_text_ienumerator;
    public int add_timer_integer;
    private int total_timer;
    private bool calculate_max_timer;
    // Start is called before the first frame update
    void Start()
    {
        rui = GameObject.Find("Heart_Score").GetComponent<RemainUI>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        timer_score_text = this.GetComponent<Text>();
        add_timer_integer = 0;
        calculate_max_timer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rui.is_timer_reduce)
            timer_score_text.enabled = false;
        else if (rui.is_timer_reduce && calculate_max_timer)
        {
            total_timer = timer.int_time;
            calculate_max_timer = false;
        }
        else if(rui.is_timer_reduce && !calculate_max_timer)
        {
            timer_score_text.enabled = true;
            is_timer_text_start = true;
        }
            
        if (is_timer_text_start)
        {
            m_typing_text = ("Total Time  : ");
            if(!is_text_ienumerator && !is_end_typing)
                StartCoroutine(typing_text());
            if(is_end_typing)
            {
                if (add_timer_integer != 0)
                    timer_score_text.text = m_typing_text + add_timer_integer.ToString();
                else timer_score_text.text = m_typing_text;

                //if(!is_up_ienumerator && (total_timer > add_timer_integer))
                //    StartCoroutine(timer_updating());
            }
        }
    }

    IEnumerator typing_text()
    {
        is_text_ienumerator = true;
        //yield return new WaitForSeconds(2f);

        for (int i = 0; i < m_typing_text.Length; ++i)
        {
            timer_score_text.text = m_typing_text.Substring(0, i);
            if (i == m_typing_text.Length - 1)
            {
                is_end_typing = true;
                is_timer_text_start = false;
            }

            yield return new WaitForSeconds(0.05f);
        }
        is_text_ienumerator = false;
    }
}
