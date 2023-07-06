using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
namespace GDFD
{

    public class TrackMiniGame : MiniGame
    {
        [Header("Setting  Track MiniGame")]
        public List<SegmentTrack> segmentTracks;

        public RectTransform[] line;
        public LineTrack[] lineTrack;
        public int indexLine;

        public float leftPos;
        public float rightPos;
        
        private List<int> _numberColor = new List<int>() { 1, 2, 3, 4 };
        [SerializeField]
        private  List<int> _currentColor = new List<int>();
        [SerializeField]
        private List<SegmentTrack> _segmentTracks = new List<SegmentTrack>();

        private void Start()
        {
            BeginMiniGame();
        }
        public override void BeginMiniGame()
        {
            base.BeginMiniGame();
            randomColor();
            randomSegment();
            SetColorsSegment();
            RandomLine();
            RandomSegmentPoint();
        }
        private void randomColor() 
        {
            for (int i = 0; i < 4; i++)
            {
                Debug.Log(i);
                int t_index = Random.Range(0,_numberColor.Count-1);
                int t_number = _numberColor[t_index];
                _currentColor.Add(t_number);
                _numberColor.Remove(t_number);

            }
            
        }

        private void randomSegment() 
        {
            for (int i = 0; i < 4; i++)
            {
                Debug.Log(i);
                int t_index = Random.Range(0, segmentTracks.Count - 1);
                SegmentTrack t_segment = segmentTracks[t_index];
                _segmentTracks.Add(t_segment);
                segmentTracks.Remove(t_segment);

            }
        }
        private void SetColorsSegment() 
        {
           
            for (int i = 0; i <= _segmentTracks.Count - 1; i++)
            {
                if (i == 0)
                {
                    Debug.Log(_segmentTracks[i].name);
                    _segmentTracks[i].SetIndex(i, _currentColor[i]);
                    
                }
                else
                {
                    _segmentTracks[i].SetIndex(_currentColor[i - 1], _currentColor[i]);
                }
                _segmentTracks[i].SetCanvas();
            }
        }
        private void RandomLine() 
        {
            int t_index = Random.Range(0,line.Length-1);
            line[t_index].gameObject.SetActive(true);
            indexLine = t_index;
        }
        private void RandomSegmentPoint() 
        {
            SegmentTrack t_segment = _segmentTracks[indexLine];
            SegmentTrack t1_segment = _segmentTracks[0];
            _segmentTracks[0] = t_segment;
            _segmentTracks[indexLine] = t1_segment;
            lineTrack[indexLine].FirstSegment(_segmentTracks[indexLine]);
            //lineTrack[indexLine].correctSegment.Add(_segmentTracks[indexLine]);
            //_segmentTracks[indexLine].SetRightPos();


            for (int i = 0; i < _segmentTracks.Count; i++)
            {
                if (i == indexLine)
                {
                    RectTransform segment = _segmentTracks[i].rect;
                    float t_width = segment.rect.width / 2;
                    float t_xPos = leftPos + t_width;
                    segment.anchoredPosition = new Vector2(t_xPos, line[indexLine].anchoredPosition.y);
                }
                else 
                {

                    RectTransform segment = _segmentTracks[i].rect;
                    float t_width = segment.rect.width / 2;
                    float t_xPos = Random.Range(leftPos + t_width, rightPos - t_width);
                    segment.anchoredPosition = new Vector2(t_xPos, line[i].anchoredPosition.y);
                }
              
            }

        }

    }
}