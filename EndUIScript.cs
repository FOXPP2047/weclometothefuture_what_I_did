using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUIScript : MonoBehaviour
{
    [HideInInspector]
    public bool trigger;
    public GameObject End_BG;
    public GameObject player;
    private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        trigger = false;
        controller = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.is_stage_ended)
        {
            trigger = true;
            End_BG.SetActive(true);
        }
            
    }
}
