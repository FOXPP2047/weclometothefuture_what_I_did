using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextBounce : MonoBehaviour
{
    // Start is called before the first frame update
    public Text title;
    private float init_scale_x;
    private float max_size;
    public float update_size;
    private bool is_ieum;
    private bool size_up;
    void Start()
    {
        init_scale_x = title.rectTransform.localScale.x;
        max_size = 1.5f;
        is_ieum = false;
        size_up = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!is_ieum && size_up)
            StartCoroutine(text_bouncing());
    }

    IEnumerator text_bouncing()
    {
        is_ieum = true;

        if(title.rectTransform.localScale.x <= max_size && size_up)
        {
            title.rectTransform.localScale += new Vector3(update_size, update_size, 0);

            if (title.rectTransform.localScale.x >= max_size)
                size_up = false;
            yield return new WaitForSeconds(0.1f);
        }

        //else if(init_scale_x < title.rectTransform.localScale.x && !size_up)
        //{
        //    title.rectTransform.localScale -= new Vector3(update_size, update_size, 0);

        //    if (title.rectTransform.localScale.x <= init_scale_x)
        //        size_up = true;
        //    yield return new WaitForSeconds(0.1f);
        //}
        is_ieum = false;
    }
}
