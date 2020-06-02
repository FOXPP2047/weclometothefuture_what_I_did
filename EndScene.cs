using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public GameObject player;
    private PlayerController cc;
    private bool is_typing_ended;
    private bool is_enum_checker;
    private bool is_blink_checker;

    private Text mission_complete_text;
    private string m_typing_text;
    [HideInInspector]
    public bool is_end_all;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        cc = player.GetComponent<PlayerController>();
        mission_complete_text = this.GetComponent<Text>();
        is_typing_ended = false;
        is_enum_checker = false;
        is_blink_checker = false;
        is_end_all = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cc.is_stage_ended)
            mission_complete_text.enabled = false;
        else if (cc.is_stage_ended && !is_typing_ended)
        {
            mission_complete_text.enabled = true;

            if (!is_enum_checker)
            {
                m_typing_text = ("MISSION COMPLETE ");
                StartCoroutine(typing_text());
            }
        }
        else if (cc.is_stage_ended && is_typing_ended && !is_end_all)
        {
            if (!is_blink_checker)
                StartCoroutine(blinK_text());
            if(duration <= 0f)
            {
                is_end_all = true;
                is_blink_checker = true;
            }

        }
        else if (is_end_all)
            mission_complete_text.enabled = true;
    }

    IEnumerator typing_text()
    {
        is_enum_checker = true;

        for (int i = 0; i < m_typing_text.Length; ++i)
        {
            mission_complete_text.text = m_typing_text.Substring(0, i);
            if (i == m_typing_text.Length - 1)
                is_typing_ended = true;
            yield return new WaitForSeconds(0.1f);
        }
        is_enum_checker = false;
    }

    IEnumerator blinK_text()
    {
        is_blink_checker = true;
        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            mission_complete_text.enabled = !mission_complete_text.enabled;
            yield return new WaitForSeconds(0.3f);
        }
        is_blink_checker = false;
    }
}
