using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip clip,get;
    // Start is called before the first frame update
    void Start()
    {
        //m.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.GetComponent<AudioSource>().clip = get;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
