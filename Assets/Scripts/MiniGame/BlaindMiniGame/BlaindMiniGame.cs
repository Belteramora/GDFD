using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDFD
{
    public class BlaindMiniGame : MiniGame
    {
        
        [Header("Setting Blaind MiniGame ")]
        [Range(0f, 760f)]
        public float width;
        [Range(0f, 760f)]
        public float stepSwipe;


        public float speedConnector;


        [Space]
        public RectTransform centerRight;
        public RectTransform centerLeft;
  
        public RectTransform[] connectorsRight;
        public RectTransform[] connectorsLeft;


        private float _maxWidth=760;
        private float _distance;
        private float _width;

        private void Start()
        {
           BeginMiniGame();  
        }
        public override void BeginMiniGame()
        {
            base.BeginMiniGame();
            ChangeWidth();

        }
        public void Swipe() 
        {
            width += stepSwipe;
            width = Mathf.Clamp(width, 0, 760);
        }
       
        public  void FixedUpdate()
        {
            if (!isMiniGameEnded) 
            {
                _width = Mathf.MoveTowards(_width, width, speedConnector * Time.deltaTime);
                ChangeWidth();
                if (_width >= _maxWidth)
                {
                    MiniGameEnded();
                }
            }
        }


        private void ChangeWidth() 
        {
            _distance = _width / connectorsRight.Length;
            for (int i = 0; i < connectorsRight.Length; i++)
            {
                connectorsRight[i].anchoredPosition = new Vector2(centerRight.anchoredPosition.x - _distance * i, connectorsRight[i].anchoredPosition.y);
                connectorsLeft[i].anchoredPosition = new Vector2(centerLeft.anchoredPosition.x + _distance * i, connectorsLeft[i].anchoredPosition.y);
            }
        }

      
    }
}
