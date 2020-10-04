using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_get : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventCenter.AddEvent("PlayMusic", PlayMusic);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //this.GetComponent<AudioSource>().clip = get;
            GameEventCenter.DispatchEvent("PlayMusic");
        }
    }

    void PlayMusic()
    {
        this.GetComponent<AudioSource>().Play();
    }
}
