using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InvincibleText : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController controller;
    private Text invincible_mode_text;
    void Start()
    {
        controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        invincible_mode_text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.invincible_mode)
            invincible_mode_text.enabled = true;
        else invincible_mode_text.enabled = false;
    }
}
