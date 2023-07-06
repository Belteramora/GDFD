using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GDFD
{
    public class FlyMove : MonoBehaviour
    {
        public Transform[] transforms;
        public float timeDelay;
        public float speed;
        public Animator anim;

        public EventTrigger eventTrigger;
        [HideInInspector]
        public FlyHitMiniGame miniGame;

        private bool _stoped;
        private bool _startTimer;
        private bool _reverse = false;
        private int _index = 0;
        private Transform _transform;
        private float _startTime = 0;

        void OnEnable()
        {
            _transform = transform;
        }
        private void MoveAndStoped()
        {

            if (!_stoped)
            {
                anim.SetBool("idle", false);
                _startTime = Time.time;
                _transform.position = Vector3.MoveTowards(_transform.position, transforms[_index].position, speed * Time.deltaTime);
                if (Vector2.Distance(_transform.position, transforms[_index].position) < 0.2f)
                {
                    _stoped = true;

                }

            }
            else
            {
                anim.SetBool("idle", true);
                StartTimer();
            }
        }
        private void StartTimer()
        {
            if (!_startTimer)
            {
                _startTime = Time.time;
                _startTimer = true;
            }

            if (Time.time >= _startTime + timeDelay)
            {
                _stoped = false;
                _startTimer = false;


                CalculatNextPoint();

            }
        }
        private void CalculatNextPoint()
        {
            if (!_reverse)
            {
                if (_index == transforms.Length - 1)
                {
                    _reverse = true;
                    _index--;
                }
                else
                {
                    _index++;
                }
            }
            else
            {
                if (_index == 0)
                {
                    _index++;
                    _reverse = false;
                }
                else
                {
                    _index--;
                }
            }
        }
        private void RotationToPoint()
        {
            Vector2 pointPos = transforms[_index].position;
            Vector2 direction = pointPos - (Vector2)_transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        private void FixedUpdate()
        {
            MoveAndStoped();
            RotationToPoint();
            RotationToPoint();
        }

        public void HitFly()
        {
            miniGame.HitFly(_transform.position) ;
            gameObject.SetActive(false);

        }
    }
}
