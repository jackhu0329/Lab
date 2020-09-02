using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    public static int gameStatus = 0;
    private float timeStart = 0f;
    private float timeEnd = 0f;
    void Start()
    {
       // timeStart = Mathf.FloorToInt(Time.time);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameStatus++;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //timeEnd = Mathf.FloorToInt(Time.time);

            Debug.Log("timer:" + timeEnd);
        }
    }

    void OnGUI()
    {

        GUIStyle gameUI= new GUIStyle();
        gameUI.normal.textColor = new Color(255, 255, 255);
        gameUI.fontSize = 30;

        GUI.Label(new Rect(Screen.width / 10 * 1, (Screen.height / 6 * 5), 200, 100),
        "已完成"+ScoreManager.score+"盤"
        , gameUI);


        if (gameStatus==0)
        {
            // GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            //     " 遊戲說明:\n" + "1.以手把靠近平底鍋和盤子\n" + "2.食指按鈕拿起物品\n" + "3.選轉手臂將平底鍋上食物放入盤子\n" + "4.將盤子移出畫面完成動作"
            //     , mymainUI);
             GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
                 " 遊戲開始!!\n"+"請將手把靠近平底鍋並按住食指按鈕拿起"
                 , gameUI);
            //mymainUI.fontSize = 200;
            //GUI.Label(new Rect(Screen.width / 2 - 50, (Screen.height / 5 * 1) + 300, 200, 100),
            //    "" + (int)(standardize_endtime - standardize_starttime), mymainUI);

        }
        else if (gameStatus == 1)
        {
            timeStart = Mathf.FloorToInt(Time.time);
            GUI.Label(new Rect(Screen.width / 10 * 2, (Screen.height / 6 * 1), 200, 100),
            " 請將另一支手把靠近盤子並按住食指按鈕拿起\n" 
            , gameUI);
        }
        else if (gameStatus == 2)
        {
            GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            " 將平底鍋移至盤子上\n"
            , gameUI);
        }
        else if (gameStatus == 3)
        {
            GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            " 旋轉平底鍋將食物放到盤子裡\n"
            , gameUI);
        }
        else if (gameStatus == 4)
        {
            GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            " 旋轉平底鍋將食物放到盤子裡\n"
            , gameUI);
        }
        else if (gameStatus == 5)
        {
            GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            " 將盤子向外移出畫面\n"
            , gameUI);
        }
        else if(gameStatus == 6)
        {
            timeEnd = Mathf.FloorToInt(Time.time)-timeStart;
            Debug.Log("timer:" + timeEnd);
            GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            " 放開雙手食指按鈕 完成動作\n"
            , gameUI);
        }

    }
}
