using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanManager : MonoBehaviour
{
    private bool inDishArea = false;
    private GameObject dish;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(-90, 0, -90);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).transform.eulerAngles.x > 300)
        {
            if (inDishArea)
            {
                //transform.GetChild(1).gameObject.SetActive(false);
                //dish.transform.GetChild(1).gameObject.SetActive(true);
                GameEventCenter.DispatchEvent("EggPass");
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "dishObject(Clone)")
        {
            dish = null;
            inDishArea = false;
        }
    }
}
