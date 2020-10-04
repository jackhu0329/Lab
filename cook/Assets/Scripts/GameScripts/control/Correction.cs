using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correction : MonoBehaviour
{
    public static bool hasCorrection = false;
    public static bool doCorrection = false;
    public static float handHeight;
    private float correctionValueY = 0f;
    private float correctionValueX = 0f;
    private float correctionValueZ = 0f;
    private float originHeight = 0f;
    public GameObject camaraRig;
    // Start is called before the first frame update
    void Awake()
    {
        //correctionValue = transform.position.y;
        originHeight = transform.position.y;
        GameEventCenter.AddEvent<Vector3>("CameraCorrection", CameraCorrection);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("correction test");
        /*if(doCorrection&& handHeight > 0)
        {
            correctionValue = handHeight - originHeight;
            //camaraRig.transform.position.y += correctionValue;
            camaraRig.transform.position=new Vector3(camaraRig.transform.position.x , camaraRig.transform.position.y+ correctionValue, camaraRig.transform.position.z);
            hasCorrection = true;
        }*/
    }

    void CameraCorrection(Vector3 v)
    {
        GameEventCenter.DispatchEvent("MotionSuccess", 0);
        Debug.Log("hand height:" + v.y);
        correctionValueY = v.y - transform.position.y;
        correctionValueX = v.x - transform.position.x;
        correctionValueZ = v.z - transform.position.z;
        camaraRig.transform.position = new Vector3(camaraRig.transform.position.x- correctionValueX, camaraRig.transform.position.y - correctionValueY, camaraRig.transform.position.z- correctionValueZ/2);
        hasCorrection = true;
    }
}
