using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameFrame
{
    public class MainSceneUI : MonoBehaviour
    {
        private float timer = 0f;
        private bool timerStatus = false;
        private bool onGUI=true;

        private int gameStatus,score = 0;
        public Text hand, dish, angle, time;
        public Button quit;

        private int test = 0;
        void Awake()
        {
            gameStatus = -1;
            transform.GetComponent<Canvas>().transform.GetChild(0).gameObject.SetActive(false);
            //goalScore = GameDataManager.FlowData.GameData.dishCount;
            if (GameDataManager.FlowData.GameData.hand == "right")
            {
                hand.text = "操作手:右手";
            }
            else
            {
                hand.text = "操作手:左手";
            }

            angle.text = "角度:" + GameDataManager.FlowData.GameData.angle;
            dish.text = "盤數:" + GameDataManager.FlowData.GameData.dishCount;

            quit.onClick.AddListener(QuitButton);
            GameEventCenter.AddEvent("TimerStart", TimerStart);
            GameEventCenter.AddEvent("TimerStop", TimerStop);
            GameEventCenter.AddEvent("GetScore", GetScore);
            GameEventCenter.AddEvent<int>("MotionSuccess", MotionSuccess);

            // timeStart = Mathf.FloorToInt(Time.time);

        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("XXXX");
            //if (ScoreManager.score == goalScore)
            if (timerStatus)
                timer += Time.deltaTime;


            if (score== GameDataManager.FlowData.GameData.dishCount)
            {
                GameEventCenter.DispatchEvent("BGMFinish");
                onGUI = false;
                
                transform.GetComponent<Canvas>().transform.GetChild(0).gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameEventCenter.DispatchEvent("GetScore");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                test = (test+1)%6;
                GameEventCenter.DispatchEvent("MotionSuccess",test);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                GameEventCenter.DispatchEvent("TimerStart");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameEventCenter.DispatchEvent("TimerStop");
                Debug.Log("timer:" + timer);
            }
        }

        void OnGUI()
        {
            if (!onGUI)
            {
                return;
            }


            GUIStyle gameUI = new GUIStyle();
            gameUI.normal.textColor = new Color(255, 255, 255);
            gameUI.fontSize = 30;

            GUI.Label(new Rect(Screen.width / 10 * 1, (Screen.height / 6 * 5), 200, 100),
            "已完成" + score + "盤"
            , gameUI);

            if (gameStatus == -1)
            {
                GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
                    "" + "手臂向前伸直並按住MENU鈕2秒進行校正"
                    , gameUI);
            }


            if (gameStatus == 0)
            {
                GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
                    "" + "請將手把靠近平底鍋並按住食指按鈕拿起"
                    , gameUI);
            }
            else if (gameStatus == 1)
            {
                TimerStart();
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
                " 手臂向外伸展將盤子向外移出畫面\n"
                , gameUI);
            }
            else if (gameStatus == 5)
            {
                GUI.Label(new Rect(Screen.width / 10 * 3, (Screen.height / 6 * 1), 200, 100),
                " 放開雙手食指按鈕 完成動作\n"
                , gameUI);
            }

        }

        public void TimerStart()
        {
            timerStatus = true;
            return;
        }

        public void TimerStop()
        {
            time.text = "花費時間:" + Mathf.FloorToInt(timer);
            timerStatus = false;
            timer = 0f;
            return;
        }

        public void MotionSuccess(int v)
        {
            gameStatus= v;
        }

        public void GetScore()
        {
            score++;
        }

        public void QuitButton()
        {
            //GameApplication.Instance.GameApplicationDispose();
            Application.Quit();
        }
    }
}

