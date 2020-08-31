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
    public Dropdown hand;

    public void Start()
    {
        launcher.SetActive(true);
        editor.SetActive(false);
        startButton.onClick.AddListener(StartButtonClick);
        settingButton.onClick.AddListener(SettingButtonClick);
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
        launcher.SetActive(true);
        editor.SetActive(false);
    }
}
