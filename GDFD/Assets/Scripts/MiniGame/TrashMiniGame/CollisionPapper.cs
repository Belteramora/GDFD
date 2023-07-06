using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDFD
{
    public class CollisionPapper : MonoBehaviour
    {
       public  bool isTouch = false;
       // bool 
        public TrashMiniGame miniGame;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!isTouch)
            {
                isTouch = true;
                //collision.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                miniGame.NextPapper();
            }
        }
    }
}