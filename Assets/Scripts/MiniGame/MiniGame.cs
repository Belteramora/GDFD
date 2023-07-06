using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GDFD
{
    /// <summary>
    /// Основной скрипт мини игр
    /// </summary>
    [System.Serializable]
    public class MiniGame : MonoBehaviour
    {
        [Header ("Setting Base MiniGame")]
        // Перечисление с указанием стадии мини игры (этапы)
        public MiniGameDirection miniGameStage;

        // Параметры типо времени и очков
        public float timeForMiniGame;
        public int maxScore;
        protected float startTime;
        protected float currentTime;
        protected bool isMiniGameEnded;
        protected bool isMiniGameStarted;

        // Эвенты юнити на обновление времени и конец мини игры
        public UnityEvent<float> onTimeUpdate;//для шкалы времени 
        [HideInInspector]
        public UnityEvent<int> onMiniGameEnded;//после окончания 

        public virtual void BeginMiniGame()
        {
            startTime = Time.time;
            isMiniGameStarted = true;
        }

        // Если время закончится, количество очков становится равным нулю
        public virtual void Update()
        {
            if (!isMiniGameEnded && isMiniGameStarted)
            {
                currentTime = Time.time - startTime;
                onTimeUpdate.Invoke(1 - currentTime / timeForMiniGame);
                if (Time.time >= startTime + timeForMiniGame)
                {
                    MiniGameEnded();//оутро перед концом 
                }
            }
        }

        // Рассчет очков на основе того, сколько времени прошло
        // Если количество очков равно нулю, вычитается одна жизнь
        public virtual void MiniGameEnded()
        {
            isMiniGameEnded = true;
           
            //оутро
            StartCoroutine(DelayOutro());
          
        }

        private IEnumerator DelayOutro() 
        {   
            yield return new WaitForSeconds(2f);
            onMiniGameEnded.Invoke(maxScore - (int)(maxScore * (currentTime / timeForMiniGame)));
            gameObject.SetActive(false);
            Destroy(gameObject, 2f); 
        }

    }
}