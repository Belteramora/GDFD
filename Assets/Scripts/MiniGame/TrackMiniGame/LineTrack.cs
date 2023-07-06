using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace GDFD
{
    public class LineTrack : MonoBehaviour, IDropHandler
    {
       
        public TrackMiniGame miniGame;
        public List<SegmentTrack> correctSegment = new List<SegmentTrack>();
        private float rectWidth;
        private float allWidth=0;
        private void OnEnable()
        {
            RectTransform rect = transform as RectTransform;
            rectWidth = (rect.position.x - rect.rect.width / 2)+70f;
        }
        public void FirstSegment(SegmentTrack firstTrack)
        {
            correctSegment.Add(firstTrack);
            firstTrack.SetRightPos();
            float width = correctSegment[0].rect.rect.width / 2;
            allWidth += rectWidth + width + width;
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                SegmentTrack segment = eventData.pointerDrag.GetComponent<SegmentTrack>();
                if (correctSegment.Count == 0)
                {
                    if (segment.leftNumber == 0)
                    {
                        correctSegment.Add(segment);
                        Debug.Log(transform.parent.localPosition.y);
                        float width = segment.rect.rect.width / 2;
                        segment.transform.localPosition = new Vector2(rectWidth + width, transform.parent.localPosition.y);
                        segment.SetRightPos();
                        allWidth += rectWidth + width+width;
                    }
                    //Debug.Log("Dropped object was: " + eventData.pointerDrag);
                }
                else 
                {
                    if (correctSegment[correctSegment.Count - 1].rightNumber == segment.leftNumber) 
                    {
                        correctSegment.Add(segment);

                        float width = segment.rect.rect.width / 2;
                        float widthLast = correctSegment[correctSegment.Count - 1].rect.rect.width / 2;
                        float xPos = correctSegment[correctSegment.Count - 1].rect.anchoredPosition.x;
                        segment.transform.localPosition = new Vector2(allWidth + width, transform.parent.localPosition.y);
                        segment.SetRightPos();
                        allWidth +=  width + widthLast;
                    }
                }
                if (correctSegment.Count == 4) 
                {
                    miniGame.MiniGameEnded();
                }
            }
        }
    }
}