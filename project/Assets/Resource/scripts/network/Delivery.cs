using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialMove
{    
    public class Delivery : MonoBehaviour
    {
        static public int[] joyStick = new int[5];
        static public string[] skillName= new string[5];
        private void Start()
        {
            joyStick[0] = 999;
            joyStick[1] = 999;
            joyStick[2] = 999;
            joyStick[3] = 999;
            joyStick[4] = 999;
        }
    }
}