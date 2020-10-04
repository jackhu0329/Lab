using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip clip,get;
    private bool hasPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        GameEventCenter.AddEvent("BGMFinish", BGMFinish);
        //m.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // this.GetComponent<AudioSource>().clip = clip;
            // this.GetComponent<AudioSource>().Play();

            GameEventCenter.DispatchEvent("BGMFinish");
        }
    }

    void BGMFinish()
    {
        if (!hasPlay)
        {
            this.GetComponent<AudioSource>().clip = clip;
            this.GetComponent<AudioSource>().Play();
            hasPlay = true;
        }

    }
}
