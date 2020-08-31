using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManager : MonoBehaviour
{
    private GameObject pan;
    private GameObject dish;
    private int status = 0;//0:in pan ,1:in dish ,2:finish

    private Vector3 posStart, posEnd;
    public float moveSpeed = 2; // 實際速度
    public float moveSpeedFixed = 2; // 移動速度
    public float jumpTime = 0.5f; // 起始點-終點的總時間
    private float jumpTimer;
    private bool jumpInit = true;
    private bool eggPassed = false;
    private int passStatus = 0;//0:suspend 1:playing 2:finish
    


    // Start is called before the first frame update
    void Start()
    {
        GameEventCenter.AddEvent("EggPass", EggPass);

    }

    // Update is called once per frame
    void Update()
    {
        pan = GameObject.Find("panObject(Clone)");
        dish = GameObject.Find("dishObject(Clone)");
        
        //  Debug.Log("test egg:" + pan + dish);
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("test pan egg:"+pan.transform.GetChild(1).gameObject.activeSelf );
            Debug.Log("test dish egg:"+ dish.transform.GetChild(1).gameObject.activeSelf);
        }
    }

    private void EggPass()
    {


        posStart = pan.transform.GetChild(1).position;
        posEnd = dish.transform.GetChild(1).position;


        if(jumpInit)
            Jump();
 
        eggPassed = true;
        //pan.transform.GetChild(1).gameObject.SetActive(false);
        //dish.transform.GetChild(1).gameObject.SetActive(true);
    }

    void Jump()
    {
        jumpTimer += Time.deltaTime * (moveSpeed / moveSpeedFixed);
        float f1 = jumpTimer / jumpTime;
        float f2 = jumpTimer - jumpTimer * f1; // 豎直加速運動
        Vector3 v1 = Vector3.Lerp(posStart, posEnd, f1); // 水平勻速運動
        pan.transform.GetChild(1).position = v1 + f2 * Vector3.up;
        if (jumpTimer >= jumpTime)
        {
            
            jumpTimer = 0;
            jumpInit = false;
            pan.transform.GetChild(1).gameObject.SetActive(false);
            dish.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
