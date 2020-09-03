using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public static int gameStatus = 0;
    private MyGameData data = new MyGameData();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test data:" + data.hand);
        GameEventCenter.AddEvent("GameInit", GameInit);
        GameEventCenter.AddEvent("ScoreGet", ScoreGet);
        GameEventCenter.AddEvent("InitStatus", InitStatus);
        GameEventCenter.AddEvent("NextStatus", NextStatus);
        GameEventCenter.AddEvent("RedoStatus", RedoStatus); 
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log("test2:" + score);
    }

    public void InitStatus()
    {
        gameStatus = 0;
    }

    public void NextStatus()
    {
        gameStatus++;
    }

    public void RedoStatus()
    {
        gameStatus--;
    }
}
