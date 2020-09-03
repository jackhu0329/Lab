using RootMotion.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class hand : MonoBehaviour
{
    public SteamVR_Action_Boolean mGrabAction = null;
    private SteamVR_Behaviour_Pose mPose = null;
    private FixedJoint mJoint = null;

    private Interactable mCurrentInteractable = null;
    private List<Interactable>  mContactInteractables = new List<Interactable>();

    public GameObject pan;
    private GameObject pickObject = null;

    public int testHand;
    private bool controlEnable = false;
    private bool inCheckArea = false;
    private int angle;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(" INIT ");
        //originPosition = pan.transform.position;
       // originRotation = pan.transform.rotation;
        mPose = GetComponent<SteamVR_Behaviour_Pose>();
        mJoint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!controlEnable)
            return;*/
        //  Debug.Log("mPose.transform.eulerAngles:"+ mPose.transform.eulerAngles);
        //  Debug.Log("pan.transform.eulerAngles:" + pan.transform.eulerAngles.x);
        if (!Correction.hasCorrection)
        {
            float timeStart =0f;
            bool timerUse = false;
            if (mGrabAction.GetStateDown(mPose.inputSource))
            {
                if (!timerUse)
                {
                    timeStart = Mathf.FloorToInt(Time.time);
                    timerUse = true;
                }
                if (Mathf.FloorToInt(Time.time) > timeStart)
                {
                    Correction.handHeight = transform.position.y;
                    Correction.doCorrection = true;
                }
            }
            if (mGrabAction.GetStateUp(mPose.inputSource))
            {
                timerUse = false;

            }

            return;
        }




        CheckRotation();
        if (mGrabAction.GetStateDown(mPose.inputSource))
        {
            Debug.Log(mPose.inputSource + " down ");
            Pickup();
        }
        if (mGrabAction.GetStateUp(mPose.inputSource))
        {
            Debug.Log(mPose.inputSource + " up ");
            Drop();
            if (ScoreManager.gameStatus == 5)
            {
                GameEventCenter.DispatchEvent("InitStatus");
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("OnTriggerEnter 1 ");
            return;
        }
        pickObject = other.gameObject;
       // Debug.Log("OnTriggerEnter 2 ");
        mContactInteractables.Add(other.gameObject.GetComponent<Interactable>());

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }
        pickObject = null;
      //  Debug.Log("OnTriggerExit  ");
        mContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    private void Pickup()
    {
        mCurrentInteractable = GetNearestInteractable();

        if (!mCurrentInteractable)
            return;

        if (mCurrentInteractable.mActiveHand)
            mCurrentInteractable.mActiveHand.Drop();

        //mCurrentInteractable.transform.position =new Vector3(transform.position.x - 0.2f, transform.position.y-0.2f, transform.position.z);
        //mCurrentInteractable.transform.eulerAngles = new Vector3(transform.rotation.x , 0 , -90);

        

        if (mCurrentInteractable.gameObject.name == "panObject(Clone)")
        {
            mCurrentInteractable.transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y - 0.1f, transform.position.z);
            mCurrentInteractable.transform.eulerAngles = new Vector3(transform.rotation.x - 90, 0, -90);
            PickUpStatusControl(mCurrentInteractable);
        }
        else if (mCurrentInteractable.gameObject.name == "dishObject(Clone)")
        {
            mCurrentInteractable.transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y - 0.1f, transform.position.z);
            mCurrentInteractable.transform.eulerAngles = new Vector3(0, 0, 0);
            PickUpStatusControl(mCurrentInteractable);
        }

        Rigidbody targetBody = mCurrentInteractable.GetComponent<Rigidbody>();
        mJoint.connectedBody = targetBody;

        mCurrentInteractable.mActiveHand = this;
    }

    private void Drop()
    {
        if (!mCurrentInteractable)
            return;

        /*mCurrentInteractable.transform.position = originPosition;
        mCurrentInteractable.transform.rotation = originRotation;*/
        if(mCurrentInteractable.gameObject.name == "panObject(Clone)")
        {
            GameEventCenter.DispatchEvent("SpawnPan");
        }
        else if (mCurrentInteractable.gameObject.name == "dishObject(Clone)")
        {
            GameEventCenter.DispatchEvent("SpawnDish");
        }
        Destroy(mCurrentInteractable.gameObject);
        GameEventCenter.DispatchEvent("InitStatus");
        /*Rigidbody targetBody = mCurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = mPose.GetVelocity();
        targetBody.angularVelocity = mPose.GetAngularVelocity();*/

        //Destroy(mCurrentInteractable.transform.gameObject);//destroy the object after drop
        mJoint.connectedBody = null;
        mCurrentInteractable.mActiveHand = null;
        mCurrentInteractable = null;


    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interactable interactive in mContactInteractables)
        {
            distance = (interactive.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance && distance < 0.1f && interactive.tag == ("Interactable"))//手把真的有碰到物體 且物體還是可以動的狀態
            {
                minDistance = distance;
                nearest = interactive;

            }

        }
        //Debug.Log("GetNearestInteractable:  "+ nearest.gameObject.name);

        return nearest;
    }

    public void ControlEnable()
    {
        controlEnable = true;
    }
    private void CheckRotation()
    {
        // if(mCurrentInteractable.gameObject.name == "panObject")
        // {

            
           // Debug.Log("CheckRotation get!!!");
      //  }
        /*if (transform.eulerAngles == angle)
        {
            Debug.Log("");
        }*/
    }

    private void PickUpStatusControl(Interactable interactable)
    {
        if (ScoreManager.gameStatus == 0 && interactable.gameObject.name == "panObject(Clone)")
        {
            GameEventCenter.DispatchEvent("NextStatus");
        }
        else if (ScoreManager.gameStatus == 1 && interactable.gameObject.name == "dishObject(Clone)")
        {
            GameEventCenter.DispatchEvent("NextStatus");
        }
    }
}
