using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RemainUI : MonoBehaviour
{
    public ParticleSystem heart_particle;
    private Text remain_heart;
    public Text end_scene;
    private EndScene es;

    private PlayerController controller;
    private string m_typing_text;
    private bool is_check_ienumerator;
    private bool is_end_typing;

    public GameObject current_heart;
    private GameObject target;
    private int current_life;
    [HideInInspector]
    public int add_heart_integer;

    [HideInInspector]
    public bool is_timer_reduce;
    private Canvas p_canvas;
    // Start is called before the first frame update
    void Start()
    {
        remain_heart = this.GetComponent<Text>();
        controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        is_check_ienumerator = false;
        is_end_typing = false;
        target = GameObject.Find("HeartCollector");
        es = end_scene.GetComponent<EndScene>();
        current_life = controller.life;
        add_heart_integer = 0;
        is_timer_reduce = false;
        p_canvas = GetComponentInParent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        //p_canvas.renderMode = RenderMode.ScreenSpaceCamera;
        if (!es.is_end_all)
            remain_heart.enabled = false;
        else if (es.is_end_all && !is_end_typing)
        {
            current_life = controller.life;
            remain_heart.enabled = true;

            if(!is_check_ienumerator)
            {
                m_typing_text = ("Remain Life : ");
                StartCoroutine(typing_text());
            }
        }

        else if(is_end_typing)
        {
            if (add_heart_integer != 0)
                remain_heart.text = m_typing_text + add_heart_integer.ToString();
            else remain_heart.text = m_typing_text;

            if (current_life > 0)
            {
                p_canvas.renderMode = RenderMode.ScreenSpaceCamera;
                move_heart_to_traget(current_heart.GetComponent<Life>().hearts[current_life - 1].transform.position, target.transform.position);
            }

            else
            {
                p_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                is_timer_reduce = true;
            }
        }
    }

    IEnumerator typing_text()
    {
        is_check_ienumerator = true;
        //yield return new WaitForSeconds(2f);

        for(int i = 0; i < m_typing_text.Length; ++i)
        {
            remain_heart.text = m_typing_text.Substring(0, i);
            if (i == m_typing_text.Length - 1)
                is_end_typing = true;
            yield return new WaitForSeconds(0.05f);
        }
        is_check_ienumerator = false;
    }

    void move_heart_to_traget(Vector3 current, Vector3 destination)
    {
        if (Vector3.Distance(current, destination) > 5f)
        {
            Vector3 move_direction = new Vector3(destination.x - current.x, destination.y - current.y, destination.z - current.z);
            move_direction.Normalize();

            current_heart.GetComponent<Life>().hearts[current_life - 1].transform.position += ((move_direction) * 500 * Time.deltaTime);
        }
        else
        {
            if (current_life >= 0)
            {
                Destroy(current_heart.GetComponent<Life>().hearts[current_life - 1]);
                ParticleSystem h_effect = Instantiate(heart_particle, destination, Quaternion.identity) as ParticleSystem;
                current_life -= 1;
                add_heart_integer += 1;
            }
        }
            
    }
}
