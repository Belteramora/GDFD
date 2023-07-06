using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDFD
{
    public class CactusMiniGame : MiniGame
    {
        [Header("Setting Cactuc MiniGame")]
        public Animator anim;

        public float currTime;//от 0 до 10

        public float minTime;
        public float maxTime;

        void Start()
        {
            BeginMiniGame(); 
        }

        public void StartShower() 
        {
            anim.SetFloat("Multiply",1);
        }
        public void StopShower()
        {
            anim.SetFloat("Multiply", -1);
        }
    }
}