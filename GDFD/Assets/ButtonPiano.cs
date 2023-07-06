using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDFD
{
    public class ButtonPiano : MonoBehaviour
    {
        public PianoMiniGame miniGame;


        public void StartAnimation() 
        {
            miniGame.StartAnimation();
        }
    }
}