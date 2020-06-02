using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerScore : MonoBehaviour
{
    // Start is called before the first frame update

    private bool is_timer_ienumerator;

    private Timer timer;

    private TimerText tt;
    private bool is_timer_start;
    [HideInInspector]
    public bool is_timer_score_ended;
    void Start()
    {
        is_timer_ienumerator = false;
        timer = this.GetComponent<Timer>();
        is_timer_start = false;
        tt = GameObject.Find("Timer_Score").GetComponent<TimerText>();
        is_timer_score_ended = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(tt.is_end_typing)
            is_timer_start = true;

        if(is_timer_start)
        {
            if (tt.is_end_typing && timer.int_time > 0)
            {
                if(!is_timer_ienumerator)
                    StartCoroutine(timer_reducing());
            }
            else if (tt.is_end_typing && timer.int_time <= 0)
                is_timer_score_ended = true;
        }         
    }
    IEnumerator timer_reducing()
    {
        is_timer_ienumerator = true;
        //yield return new WaitForSeconds(2f);

        timer.int_time -= 1;
        tt.add_timer_integer += 1;
        yield return new WaitForSeconds(0.07f);
       
        is_timer_ienumerator = false;
    }
}
