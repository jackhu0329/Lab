using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    private bool foodActive = false;
    private Vector3 originPosition = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Awake()
    {
        transform.GetChild(1).gameObject.SetActive(false);

    }



    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(1).gameObject.activeSelf)
        {
            foodActive = true;
        }
        else
        {
            foodActive = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "CheckArea")
        {
            if(foodActive)
            {
                SuccessMotion();
            }
            
        }
    }

    private void SuccessMotion()
    {
        Debug.Log("SuccessMotion");
        transform.GetChild(1).gameObject.SetActive(false);
        GameEventCenter.DispatchEvent("ScoreGet");


        if (ScoreManager.gameStatus == 4)
        {
            GameEventCenter.DispatchEvent("NextStatus");
        }

        /*GameEventCenter.DispatchEvent("SpawnDish");
        
        Destroy(transform.gameObject);*/

    }
}
