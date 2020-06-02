using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffect : MonoBehaviour
{
    private BoxCollider platform_collider;
    private GameObject player;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        platform_collider = GetComponent<BoxCollider>();
        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - GetComponent<BoxCollider>().bounds.extents.x <= controller.transform.position.x &&
           player.transform.position.x <= transform.position.x + GetComponent<BoxCollider>().bounds.extents.x &&
           controller.transform.position.y - controller.GetComponent<CharacterController>().height * 0.5f - 0.5f < transform.position.y + GetComponent<BoxCollider>().bounds.extents.y)
        {
            this.gameObject.layer = 16;
        }

        if (transform.position.x - GetComponent<BoxCollider>().bounds.extents.x <= player.transform.position.x &&
           player.transform.position.x <= transform.position.x + GetComponent<BoxCollider>().bounds.extents.x &&
           controller.transform.position.y - controller.GetComponent<CharacterController>().height * 0.5f - 0.5f > transform.position.y + GetComponent<BoxCollider>().bounds.extents.y)
        {
            this.gameObject.layer = 17;
        }

        if (controller.down_jump)
        {
            this.gameObject.layer = 16;
        }
    }

}
