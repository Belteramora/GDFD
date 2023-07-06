using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDFD
{
    /// <summary>
    /// Мини игра про включение ПК
    /// </summary>
    public class PCMiniGame : MiniGame
    {
        [Header("Setting PC MiniGame")]
        public bool isOnButtonEnabled;

        public Animator anim;
        public Image onButton;
        public Sprite defaultSprite;
        public Sprite enableSprite;
        [Range (1,10)]
        public int minRangeHit;
        [Range(1, 10)]
        public int maxRangeHit;
        private int neededHit = 0;
        private int currentHitToOn=0;
        
        public override void BeginMiniGame()
        {
            base.BeginMiniGame();
            RandomHit();
            onButton.sprite = defaultSprite;
        }

        // Если нажимаем когда кнопка горит красным - мы проиграли
        // Если нажимаем когда кнопка горит зеленым - выиграли
        public void OnOnButtonClicked()
        {
            if (!isMiniGameEnded) 
            {
                if (isOnButtonEnabled)
                {
                    MiniGameEnded();
                }
                else
                {
                    currentTime = timeForMiniGame;
                    MiniGameEnded();
                }
            }
        }

        // При ударе по корпусу есть шанс что кнопка загорится зеленым
        public void OnCaseHitted()
        {
            anim.Play("Hit");
            currentHitToOn++;
            if (currentHitToOn == neededHit)
            {
                onButton.sprite = enableSprite;
                isOnButtonEnabled = true;
            }
            else if(currentHitToOn > neededHit) 
            {
                currentHitToOn = 1;
                RandomHit();
                onButton.sprite = defaultSprite;
                isOnButtonEnabled = false;
            }
        }

        public void RandomHit() 
        {
            neededHit = Random.Range(minRangeHit,maxRangeHit);
        }
    }
}
