using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Life : MonoBehaviour
{
    private GameObject player;
    private PlayerController controller;
    
    public List<GameObject> hearts;
    public GameObject h;
    public GameObject prefabs;

    private Vector3 last_pos;
    private int current_life;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        hearts = new List<GameObject>();

        Vector3 pos = transform.position;
        for (int i = 0; i < controller.life; ++i)
        {
            pos.x += prefabs.GetComponent<RectTransform>().sizeDelta.x;
            h = Instantiate<GameObject>(prefabs, new Vector3(pos.x, pos.y, 0), transform.rotation);
            h.transform.SetParent(this.transform.parent, true);
            hearts.Add(h);
        }
        last_pos = new Vector3(pos.x, pos.y, 0);
        current_life = controller.life;
    }

    // Update is called once per frame
    void Update()
    {
        if ((current_life != controller.life) && controller.life >= 0)
        {
            if(current_life > controller.life)
            {
                last_pos.x -= prefabs.GetComponent<RectTransform>().sizeDelta.x;
                Destroy(hearts[current_life - 1]);
                hearts.Remove(hearts[current_life - 1]);
                current_life = controller.life;
            }

            else if (current_life < controller.life)
            {
                last_pos.x += prefabs.GetComponent<RectTransform>().sizeDelta.x;
                h = Instantiate<GameObject>(prefabs, new Vector3(last_pos.x, last_pos.y, 0), transform.rotation);
                h.transform.SetParent(this.transform.parent, true);
                current_life = controller.life;
                hearts.Add(h);
            }
        }
    }
}
