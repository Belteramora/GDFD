using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDFD
{
    public class FindObjectMiniGame : MiniGame
    {
        [Header("Setting Find Object MiniGame")]
        public RectTransform flashlight;
        public int index=0;
        public float minDistance;
        public GameObject[] tableObjects;//стол с предметами 
        public RectTransform[] memoryStick;//стол с предметами 

       
        public override void BeginMiniGame()
        {
            base.BeginMiniGame();
            RandomObject();
        }
        private void RandomObject() 
        {
            int t_index = Random.Range(0, tableObjects.Length);
            index = t_index;
            tableObjects[index].SetActive(true);
        }
        public void CheckFindObject() 
        {
            Debug.Log("cHECK");
            Debug.Log(Vector2.Distance(memoryStick[index].position, flashlight.position));
            if (Vector2.Distance(memoryStick[index].position, flashlight.position) <=minDistance) 
            {
                MiniGameEnded(); 
                Debug.Log("нашел ");
            }
        }






    }
}

