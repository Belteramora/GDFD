using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDFD
{
    public class Floor : MonoBehaviour
    {
        public TrashMiniGame miniGame;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            miniGame.NextPapper();
        }
    }
}