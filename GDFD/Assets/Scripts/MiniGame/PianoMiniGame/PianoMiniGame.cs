using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GDFD
{
    public class PianoMiniGame : MiniGame
    {
        [Header("Setting Piano MiniGame")]
        public int[] needCombinatios;//последовательность нажатий 
        public Animator[] buttonsAnim;//анимации для проигрованиия комбинации 
        public List<int> currentCombination = new List<int>();
        public Button[] buttons;

        private int _index=0;

        private void Start()
        {
            Debug.Log("starPiano");
            StartAnimation();
        }


        public override void BeginMiniGame()
        {
            StartAnimation();
        }
        public void StartTime()
        {
            startTime = Time.time;
            isMiniGameStarted = true;
        }
        public void StartAnimation() //ссылка из аниматора кнопки , в конце каждой анимации вызваться метод , который запускает следующую кнопку и так пока все кнопки не проиграются 
        {
            //запуск следующей кнопки , если кончились , запуск таймера 
            if (_index < needCombinatios.Length)
            {
                buttonsAnim[needCombinatios[_index]].Play("Tap");
                _index++;
            }
            else if (_index == needCombinatios.Length) 
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].interactable = true;
                }
                StartTime();
                Debug.Log("пошло время");
                //пошло время 
            }
        }
        public override void MiniGameEnded()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }
            base.MiniGameEnded();
        }
        public void TouchButton(int index)
        {
            currentCombination.Add(index);
            CheckCombination();
        }
        private void CheckCombination()
        {
            if (currentCombination[currentCombination.Count - 1] != needCombinatios[currentCombination.Count - 1])
            {
                currentTime = timeForMiniGame;
                Debug.Log("Ты проиграл");
                MiniGameEnded();
            }
            if (currentCombination.Count == needCombinatios.Length)
            {
                MiniGameEnded();
                Debug.Log("Ты победил");
            }
        }

    }
}