using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace GDFD
{

    public class SegmentTrack : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image leftConnector;
        public Image rightConnector;
        public RectTransform rect;

        public int leftNumber;
        public int rightNumber;

        public Sprite[] color;

        Vector2 startPosition;
        Vector2 limits;

        public Canvas canvas;

        private bool isRightPos;

        public void SetCanvas() 
        {
            limits = canvas.GetComponent<RectTransform>().rect.max;
            rect = GetComponent<RectTransform>();
        }
        public void SetIndex(int leftIndex,int rightIndex) 
        {
            leftNumber = leftIndex;
            rightNumber = rightIndex;
            leftConnector.sprite = color[leftIndex];
            rightConnector.sprite = color[rightIndex];
        }



        public void SetRightPos() 
        {
            isRightPos = true;
            
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isRightPos)
                startPosition = transform.localPosition;
            // startPosition = eventData.position;
    

        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!isRightPos)
            {
                transform.localPosition = PosInCanvase(eventData.position);
                var p = transform.localPosition;
                if (p.x < -limits.x)
                { p.x = -limits.x; }
                if (p.x > limits.x)
                { p.x = limits.x; }
                if (p.y < -limits.y)
                { p.y = -limits.y; }
                if (p.y > limits.y)
                { p.y = limits.y; }
                transform.localPosition = p;

            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isRightPos)
            transform.localPosition = startPosition;
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
    }
}
