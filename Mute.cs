using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mute : MonoBehaviour
{
    public Text on;
    public Text off;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(on.GetComponent<Text>().IsActive())
        {
            off.GetComponent<GameObject>().SetActive(false);
        }
        else if(off.GetComponent<Text>().IsActive())
        {
            on.GetComponent<GameObject>().SetActive(true);
        }
    }
}
