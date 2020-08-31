using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 0;
    void Start()
    {
        GameEventCenter.AddEvent("ScoreInit", ScoreInit);
        GameEventCenter.AddEvent("ScoreGet", ScoreGet);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("test0");
            GameEventCenter.DispatchEvent("ScoreInit");
        }
    }

    public void ScoreInit()
    {
        Debug.Log("test1");
        score = 0;
    }

    public void ScoreGet()
    {
        Debug.Log("test2");
        score++;
        Debug.Log("test2:"+score);
    }
}
