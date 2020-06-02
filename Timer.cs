using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [HideInInspector]
    public Text timer_text;
    private float start_time;
    private PlayerController controller;

    [HideInInspector]
    public int int_time;
    // Start is called before the first frame update
    void Start()
    {
        timer_text = this.GetComponent<Text>();
        start_time = Time.time;
        controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        int_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.is_stage_ended)
        {
            float t = Time.time - start_time;

            string minutes = ((int)t / 60).ToString();
            string seconds = ((int)t % 60).ToString();

            int_time = (int)t;
            
        }

        timer_text.text = int_time.ToString();
    }
}
