using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeight : MonoBehaviour
{
    [HideInInspector] public Vector3 playerTarget;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerController>().is_ground)
        {
            playerTarget = gameObject.transform.position;
            playerTarget.y -= gameObject.transform.localScale.y;
        }
    }
}
