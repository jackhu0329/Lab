using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemManager : MonoBehaviour
{
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(item, transform.position, Quaternion.identity);
        GameEventCenter.AddEvent("SpawnPan", SpawnPan);
        GameEventCenter.AddEvent("SpawnDish", SpawnDish);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("create !");
            SpawnPan();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("create !");
            SpawnDish();
        }

    }

    private void SpawnPan()
    {
        if(item.name == "panObject")
        {
            Instantiate(item, transform.position, Quaternion.identity);
            Debug.Log("SpawnPan");
        }
    }

    private void SpawnDish()
    {
        if (item.name == "dishObject")
        {
            Instantiate(item, transform.position, Quaternion.identity);
            Debug.Log("SpawnDish");
        }
    }
}
