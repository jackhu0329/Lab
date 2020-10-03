using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame
{
    public class DishTask : TaskBase
    {
        // Start is called before the first frame update
        private GameObject dish;
        private GameObject spawnPoint;
        // Start is called before the first frame update
        public override IEnumerator TaskInit()
        {
            Debug.Log("spawn pan 12345");
            spawnPoint = GameObject.Find("SpawnDish");
            dish = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Dish.gameObject;
            GameEventCenter.AddEvent("SpawnDish", SpawnDish);
            yield return null;
        }


        public override IEnumerator TaskStart()
        {
            SpawnDish();

            yield return null;
        }


        public override IEnumerator TaskStop()
        {
            yield return null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SpawnDish()
        {
            GameObject panClone = GameObject.Instantiate(dish, spawnPoint.transform.position, Quaternion.identity);
        }
    }
}

