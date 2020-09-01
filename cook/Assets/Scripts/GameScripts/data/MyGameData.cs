using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;

namespace LabData
{
    public class MyGameData : LabDataBase
    {
        public string hand { get; set; }
        public int angle { get; set; }
        public int dishCount { get; set; }

        public MyGameData()
        {
            hand = "right";
            angle = 30;
            dishCount = 5;
        }
        public MyGameData(string handValue,int angleValue,int dishCountValue)
        {
            hand = handValue;
            angle = angleValue;
            dishCount = dishCountValue;
        }

    }
}

