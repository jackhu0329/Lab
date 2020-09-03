using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanManager : MonoBehaviour
{
    private bool inDishArea = false;
    private GameObject dish;
    private int angle;
    // Start is called before the first frame update
    void Start()
    {
        angle = GameDataManager.FlowData.GameData.angle;
        transform.eulerAngles = new Vector3(-90, 0, -90);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).transform.eulerAngles.x > 270+ angle)
        {
            if (inDishArea)
            {
                //transform.GetChild(1).gameObject.SetActive(false);
                //dish.transform.GetChild(1).gameObject.SetActive(true);
                GameEventCenter.DispatchEvent("EggPass");
                if (ScoreManager.gameStatus == 3)
                {
                    GameEventCenter.DispatchEvent("NextStatus");
                }
            }
            
            //Debug.Log("PAN MANAGER!!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PAN MANAGER name:"+ other);
        if(other.name == "dishObject(Clone)")
        {
            Debug.Log("PAN MANAGER enter!!");
            dish = other.gameObject;
            inDishArea = true;
            if (ScoreManager.gameStatus == 2)
            {
                GameEventCenter.DispatchEvent("NextStatus");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "dishObject(Clone)")
        {
            dish = null;
            inDishArea = false;
            if (ScoreManager.gameStatus == 3)
            {
                GameEventCenter.DispatchEvent("RedoStatus");
            }
        }
    }
}
