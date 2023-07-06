using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GDFD
{
    public class FlyHitMiniGame : MiniGame, IPointerDownHandler
    {
        [Header("Setting FlyHit MiniGame")]
        public RectTransform FlyHitter;
        public Animator animHitter;
        public Transform flySpawnParent;
        public List<GameObject> fly;
        public int countFlyInMonitor;



        private int _valueFly;
        private Vector2 _pos;

        
        public override void BeginMiniGame()
        {
            base.BeginMiniGame();
            _valueFly = countFlyInMonitor;
            SpawFly();
        }
        public void FlyDeath() 
        {
            _valueFly--;
            if (_valueFly <=0) 
            {
                MiniGameEnded();
            }
        }
        private void SpawFly() 
        {
            for (int i = 0; i <countFlyInMonitor; i++)
            {
                
                FlyMove go = Instantiate(fly[i], flySpawnParent).GetComponentInChildren<FlyMove>();
                go.miniGame = this;
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
                go.eventTrigger.triggers.Add(entry);
            }
        }

        private void OnPointerDownDelegate(PointerEventData data)
        {
            FlyDeath();
        }

        public void HitFly(Vector2 position) 
        {
            PositionHitter(position);


        }
        public void OnPointerDown(PointerEventData eventData)
        {
            
           // pos = eventData.pointerPressRaycast.worldPosition;
            PositionHitter(eventData.pointerPressRaycast.worldPosition);

        }
        private void PositionHitter(Vector2 position) 
        {
            if (!isMiniGameEnded)
            {
                _pos = position;
                FlyHitter.position = position;
                animHitter.Play("Hit");
            }
        }

    }
}