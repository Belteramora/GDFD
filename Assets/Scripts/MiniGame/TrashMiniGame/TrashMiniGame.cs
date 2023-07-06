using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace GDFD
{
    public class TrashMiniGame : MiniGame, IPointerUpHandler, IPointerDownHandler,IPointerMoveHandler
    {
        [Header("Setting Trash MiniGame")]
        public float minNeedPower;//необходимая сила для того чтоб бумажка долетела 
        public float maxNeedPower;//необходимая сила для того чтоб бумажка долетела 
     
        public float xLeftPos;
        public float xRightPos;

        public RectTransform front;
        public RectTransform back;

        public float timeToFly;
        public float speed;//скорость уменьшения объекта 

        public bool isWinned;
        public float minSwipeTime;
        public float maxDistanceSwipe;
        public float minDistanceSwipe;
        public Canvas canvas;
        public int index=0;
        public RectTransform[] papper;
        public Rigidbody2D[] rb;
        public GameObject frontBean;
        public List<Animator> animPapper;

       
        private bool isTap;
        private bool isThrow;
        private bool _great;
        private float _timeToSwap=0;
        private Vector2 _posTouch;
        private Vector2 _posToMove;

        
        
        public override void BeginMiniGame()
        {
            base.BeginMiniGame();
        }

        public void FixedUpdate()
        {
          
            if (isTap)
            {
                
                if (Time.time >= _timeToSwap + minSwipeTime)
                {
                    //проверка на длинну свапа 
                        EndendTap();
                }
            }
            if (isThrow) 
            {
                _posToMove = Vector2.MoveTowards(papper[index].anchoredPosition, _posTouch,speed);
                papper[index].anchoredPosition = _posToMove;
                
            }
            if ( index<papper.Length && papper[index].anchoredPosition == _posTouch)
            {
                if (_great)
                {
                    GreatThrow();
                    papper[index].SetParent(back);
                }
                else 
                {
                    papper[index].SetParent(front);
                }
                isThrow = false;
                rb[index].bodyType = RigidbodyType2D.Dynamic;
                rb[index].simulated = true;
                rb[index].mass = 2f;
            }
        }

        public void NextPapper() 
        {
            if (!isMiniGameEnded)
            {
                index++;
                if (index < papper.Length)
                {
                    frontBean.SetActive(false);
                    papper[index].gameObject.SetActive(true);
                }
                else
                {
                    MiniGameEnded();
                    index = papper.Length - 1;
                }
            }
        }
        public override void MiniGameEnded()
        {
            if (!isWinned)
            {
                currentTime = timeForMiniGame;
            }

            base.MiniGameEnded();
        }
        public void OnPointerUp(PointerEventData eventData)
        {

            if (!isThrow)
            {
                EndendTap();
            }
        }
        public void OnPointerMove(PointerEventData eventData)
        {
            if (!isThrow)
            {
                _posTouch = eventData.pointerCurrentRaycast.screenPosition;
                _posTouch = PosInCanvase(_posTouch);
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isThrow)
            {
                _posTouch = Vector2.zero;
                Vector2 t_posTouch = PosInCanvase(eventData.pointerPressRaycast.screenPosition);

                //Debug.Log(papper.anchoredPosition + " " + t_posTouch);
                if (Vector2.Distance(papper[index].anchoredPosition, t_posTouch) < 80)
                {
                    TapPapper();
                }
            }
        }
        public void TapPapper() 
        {
            isTap=true;
            _timeToSwap = Time.time;
        }
        private Vector2 PosInCanvase(Vector2 pos) 
        {
            Vector2 temp = pos;
            float h = Screen.height;
            float w = Screen.width;
            float x = temp.x - (w / 2);
            float y = temp.y - (h / 2);
            float s = canvas.scaleFactor;
            temp = new Vector2(x, y) / s;
            return temp;
        }
        public void GreatThrow() 
        { 
            if(_great)
            frontBean.SetActive(true);
        }
        public void EndendTap()
        {
            
           //papper.anchoredPosition = _posTouch;
           if (isTap)
           {
               Vector2 heading = _posTouch - papper[index].anchoredPosition;
               float distance = heading.magnitude;
               Vector2 dir = heading / distance;
               if (distance >= minDistanceSwipe)
               {
                   
                   Debug.Log(distance);
                 
                    if (distance > maxDistanceSwipe) 
                    {

                        _posTouch *= 0 / 7f;
                    }
                   if (distance > maxNeedPower || distance < minNeedPower)
                   {
                       _great = false;
                    
                        Debug.Log("НеЗаебись");
                   }
                   else if ((distance < maxNeedPower || distance > minNeedPower) && (heading.x > xLeftPos || heading.x < xRightPos))
                   {

                       _great = true;
                        
                        Debug.Log("Заебись");
                   }
                   isTap = false;
                   isThrow = true;
                   animPapper[index].Play("throw");
               }
                isTap = false;
           }
            
            
        }

      
    }
}