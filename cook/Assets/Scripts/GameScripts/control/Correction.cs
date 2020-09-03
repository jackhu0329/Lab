using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correction : MonoBehaviour
{
    public static bool hasCorrection = false;
    public static bool doCorrection = false;
    public static float handHeight;
    private float correctionValue = 0f;
   private float originHeight = 0f;
    public GameObject camaraRig;
    // Start is called before the first frame update
    void Start()
    {
        //correctionValue = transform.position.y;
        originHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("correction test");
        if(doCorrection&& handHeight > 0)
        {
            correctionValue = handHeight - originHeight;
            //camaraRig.transform.position.y += correctionValue;
            camaraRig.transform.position=new Vector3(camaraRig.transform.position.x , camaraRig.transform.position.y+ correctionValue, camaraRig.transform.position.z);
            hasCorrection = true;
        }
    }
}
