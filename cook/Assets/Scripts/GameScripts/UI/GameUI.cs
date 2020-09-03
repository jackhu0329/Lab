using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    
    private float timeStart = 0f;
    private float timeEnd = 0f;
    
    private int goalScore = 0;
    public Text hand, dish, angle, time;
    void Start()
    {
        transform.GetComponent<Canvas>().transform.GetChild(0).gameObject.SetActive(false);
        goalScore = GameDataManager.FlowData.GameData.dishCount;
        if (GameDataManager.FlowData.GameData.hand == "right")
        {
            hand.text = "操作手:右手";
        }
        else
        {
            hand.text = "操作手:左手";
        }
        
        angle.text = "角度:"+ GameDataManager.FlowData.GameData.angle;
        dish.text = "盤數:"+ GameDataManager.FlowData.GameData.dishCount;

        // timeStart = Mathf.FloorToInt(Time.time);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("XXXX");
        //if (ScoreManager.score == goalScore)
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("QQQQQ");
            timeEnd = Mathf.FloorToInt(Time.time) ;
            time.text = "花費時間" + (timeEnd - timeStart);
            transform.GetComponent<Canvas>().transform.GetChild(0).gameObject.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            ScoreManager.gameStatus++;
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


        if (ScoreManager.gameStatus == 0)
        {
            // GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            //     " 遊戲說明:\n" + "1.以手把靠近平底鍋和盤子\n" + "2.食指按鈕拿起物品\n" + "3.選轉手臂將平底鍋上食物放入盤子\n" + "4.將盤子移出畫面完成動作"
            //     , mymainUI);
             GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
                 ""+"請將手把靠近平底鍋並按住食指按鈕拿起"
                 , gameUI);
            //mymainUI.fontSize = 200;
            //GUI.Label(new Rect(Screen.width / 2 - 50, (Screen.height / 5 * 1) + 300, 200, 100),
            //    "" + (int)(standardize_endtime - standardize_starttime), mymainUI);

        }
        else if (ScoreManager.gameStatus == 1)
        {
            timeStart = Mathf.FloorToInt(Time.time);
            GUI.Label(new Rect(Screen.width / 10 * 2, (Screen.height / 6 * 1), 200, 100),
            " 請將另一支手把靠近盤子並按住食指按鈕拿起\n" 
            , gameUI);
        }
        else if (ScoreManager.gameStatus == 2)
        {
            GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            " 將平底鍋移至盤子上\n"
            , gameUI);
        }
        else if (ScoreManager.gameStatus == 3)
        {
            GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            " 旋轉平底鍋將食物放到盤子裡\n"
            , gameUI);
        }
        else if (ScoreManager.gameStatus == 4)
        {
            GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            " 手臂向外伸展將盤子向外移出畫面\n"
            , gameUI);
        }
        else if(ScoreManager.gameStatus == 5 )
        {
            
            Debug.Log("timer:" + timeEnd);
            GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
            " 放開雙手食指按鈕 完成動作\n"
            , gameUI);
        }

    }
}
