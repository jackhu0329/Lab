using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame
{
    public class PanTask : TaskBase
    {
        private GameObject pan;
        private GameObject spawnPoint;
       
        // Start is called before the first frame update
        public override IEnumerator TaskInit()
        {
            Debug.Log("spawn pan 12345");
            spawnPoint = GameObject.Find("SpawnPan");
            pan = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Pan.gameObject;
            GameEventCenter.AddEvent("SpawnPan", SpawnPan);
            yield return null;
        }


        public override IEnumerator TaskStart()
        {
            SpawnPan();

            yield return null;
        }


        public override IEnumerator TaskStop()
        {
            yield return null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SpawnPan()
        {
            GameObject panClone = GameObject.Instantiate(pan, spawnPoint.transform.position, Quaternion.identity);
        }
    }
}

