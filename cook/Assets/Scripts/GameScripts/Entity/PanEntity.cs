using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame
{
    public class PanEntity : GameEntityBase
    {
        private GameObject targetObj;
        private float time;

        private Vector3 posStart, posEnd;
        public float moveSpeed = 2; // 實際速度
        public float moveSpeedFixed = 2; // 移動速度
        public float jumpTime = 0.5f; // 起始點-終點的總時間
        private float jumpTimer;
        private bool jumpInit = true;
        public override void EntityDispose()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            targetObj = other.gameObject;
            time = 0.0f;
        }

        public void OnTriggerExit(Collider other)
        {
            targetObj = null;
            time = 0.0f;
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.name != "dishObject(Clone)")
                return;


            if (other.name == "dishObject(Clone)")
            {
                if (transform.GetChild(0).transform.eulerAngles.x > 270 + GameDataManager.FlowData.GameData.angle)
                {
                    EggPass();
                    //Debug.Log("PAN MANAGER!!");
                }
            }




            //time += Time.deltaTime;
            /*if (other.gameObject.CompareTag("Cabinet_Cup"))
                time += Time.deltaTime;

            if (time >= GameDataManager.FlowData.InduceTimeCost)
            {
                other.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
                other.gameObject.tag = "Untagged";
                GameEventCenter.DispatchEvent("ResetHandGrab");
                Destroy(this.gameObject);
                GameEventCenter.DispatchEvent("SpawnCup");
                GameEventCenter.DispatchEvent("AddScore");
                time = 0;
                GameEventCenter.DispatchEvent<AudioSelect>("AudioPlay", AudioSelect.PlaceCup);
            }*/
        }

        private void EggPass()
        {


            posStart = transform.GetChild(1).position;
            posEnd = targetObj.transform.GetChild(1).position;


            if (jumpInit)
                Jump();

            //eggPassed = true;
            //pan.transform.GetChild(1).gameObject.SetActive(false);
            //dish.transform.GetChild(1).gameObject.SetActive(true);
        }

        void Jump()
        {
            jumpTimer += Time.deltaTime * (moveSpeed / moveSpeedFixed);
            float f1 = jumpTimer / jumpTime;
            float f2 = jumpTimer - jumpTimer * f1; // 豎直加速運動
            Vector3 v1 = Vector3.Lerp(posStart, posEnd, f1); // 水平勻速運動
            transform.GetChild(1).position = v1 + f2 * Vector3.up;
            if (jumpTimer >= jumpTime)
            {

                jumpTimer = 0;
                jumpInit = false;
                transform.GetChild(1).gameObject.SetActive(false);
                targetObj.transform.GetChild(1).gameObject.SetActive(true);


            }
        }
    }
}

