using RootMotion.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using HTC.UnityPlugin.Vive;
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
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

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

            if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Menu))
            {
                timer += Time.deltaTime;
                if (timer >= 3.0f )
                {
                    //GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().mainSceneUI.SetUIActive(0, false);
                    //GameEventCenter.DispatchEvent<Vector3>(EventName.EnableCameraRig, this.transform.position);
                    GameEventCenter.DispatchEvent("CameraCorrection",transform.position);
                    Correction.hasCorrection = true;
                }
            }

            if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Menu))
            {
                timer += Time.deltaTime;
                if (timer >= 3.0f)
                {
                    //GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().mainSceneUI.SetUIActive(0, false);
                    //GameEventCenter.DispatchEvent<Vector3>(EventName.EnableCameraRig, this.transform.position);
                    Correction.hasCorrection = true;
                }
            }

            return;
        }


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
            Debug.Log("1003 1 ");
            return;
        }
        pickObject = other.gameObject;

        mContactInteractables.Add(other.gameObject.GetComponent<Interactable>());

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }
        pickObject = null;

        mContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    private void Pickup()
    {
        mCurrentInteractable = GetNearestInteractable();
        Debug.Log("1003 2 ");
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
        GameEventCenter.DispatchEvent("MotionSuccess", 0);
        Destroy(mCurrentInteractable.gameObject);
        mContactInteractables = new List<Interactable>();
        //GameEventCenter.DispatchEvent("InitStatus");

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
                Debug.Log("1003 1 ");
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



    private void PickUpStatusControl(Interactable interactable)
    {
        if (interactable.gameObject.name == "panObject(Clone)")
        {
            GameEventCenter.DispatchEvent("MotionSuccess", 1);
        }
        else if ( interactable.gameObject.name == "dishObject(Clone)")
        {
            GameEventCenter.DispatchEvent("MotionSuccess", 2);
        }
    }
}
