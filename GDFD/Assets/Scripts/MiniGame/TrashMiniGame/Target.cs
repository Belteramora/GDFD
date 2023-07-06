using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDFD
{

    public class Target : MonoBehaviour
    {

        public TrashMiniGame miniGame;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollisionPapper>().isTouch == false)
            {
                //collision.GetComponent<Collider>().GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                miniGame.isWinned = true;
                miniGame.MiniGameEnded();
            }
        }

    }
}