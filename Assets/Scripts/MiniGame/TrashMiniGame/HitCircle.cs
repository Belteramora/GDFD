using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDFD
{
    public class HitCircle : MonoBehaviour
    {
        public TrashMiniGame miniGame;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //miniGame.GreatThrow();
        }
    }
}
