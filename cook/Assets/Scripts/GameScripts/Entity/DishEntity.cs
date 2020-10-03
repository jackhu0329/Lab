using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame
{
    public class DishEntity : GameEntityBase
    {


        public override void EntityDispose()
        {

        }

        private void OnTriggerStay(Collider other)
        {
            
            if (other.gameObject.name == "CheckArea")
            {
                
                if (transform.GetChild(1).gameObject.activeSelf)
                {
                    
                    GameEventCenter.DispatchEvent("GetScore");
                    transform.GetChild(1).gameObject.SetActive(false);
                    GameEventCenter.DispatchEvent("MotionSuccess", 5);
                }

            }
        }

    }

}
