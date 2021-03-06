﻿using LabData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;
    private MyGameData data = new MyGameData(); 
    void Start()
    {
        
        Debug.Log("test data:"+data.hand);
        GameEventCenter.AddEvent("GameInit", GameInit);
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

    public void GameInit()
    {
        Debug.Log("test1");
        data = GameDataManager.FlowData.GameData;
        score = 0;
    }

    public void ScoreGet()
    {
        Debug.Log("test2");
        score++;
        Debug.Log("test2:"+score);
    }
}
