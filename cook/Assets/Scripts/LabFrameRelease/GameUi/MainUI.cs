using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public GameObject launcher;
    public GameObject editor;
    public Button startButton;
    public Button settingButton;
    public Button settingFinishButton;
    public Dropdown hand,angle,dish;
    public InputField scriptName;

    public void Start()
    {
        LabTools.CreateDataFolder<MyGameData>();
        launcher.SetActive(true);
        editor.SetActive(false);
        startButton.onClick.AddListener(StartButtonClick);
        settingButton.onClick.AddListener(SettingButtonClick);
        settingFinishButton.onClick.AddListener(SettingFinishButtonClick);
    }

    public void StartButtonClick()
    {
        GameFlowData gameFlow = new GameFlowData();

        GameDataManager.FlowData = gameFlow;

        var Id = gameFlow.UserId;

        //GameDataManager.LabDataManager.LabDataCollectInit(() => Id);
        GameSceneManager.Instance.Change2MainScene();
    }

    public void SettingButtonClick()
    {
        launcher.SetActive(false);
        editor.SetActive(true);

    }

    public void SettingFinishButtonClick()
    {
        MyGameData gameData = new MyGameData();
        SetData(gameData);

        if (scriptName.text != null || scriptName.text != "")
        {
            LabTools.WriteData(gameData, scriptName.text, true);
        }
        else
        {
            LabTools.WriteData(gameData, "default", true);
        }
        //LabTools.WriteData(gameData, "test123",true);
        Debug.Log("dropdown data:" + gameData.hand);
        Debug.Log("dropdown data:" + gameData.angle);
        Debug.Log("dropdown data:" + gameData.dishCount);
        launcher.SetActive(true);
        editor.SetActive(false);
    }

    private void SetData(MyGameData data)
    {
        if(hand.value == 0)
        {
            data.hand = "right";
        }
        else if (hand.value ==1)
        {
            data.hand = "left";
        }

        if(angle.value == 0)
        {
            data.angle = 10;
        }
        else if (angle.value == 1)
        {
            data.angle = 30;
        }
        else if (angle.value == 2)
        {
            data.angle = 90;
        }
        else if (angle.value == 3)
        {
            data.angle = 180;
        }

        if (dish.value == 0)
        {
            data.dishCount = 5;
        }
        else if (dish.value == 1)
        {
            data.dishCount = 10;
        }
        else if (dish.value == 2)
        {
            data.dishCount = 15;
        }
    }

    
}
