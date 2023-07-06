using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDFD
{
    /// <summary>
    /// ���� ���� ��� ��������� ��
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

        // ���� �������� ����� ������ ����� ������� - �� ���������
        // ���� �������� ����� ������ ����� ������� - ��������
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

        // ��� ����� �� ������� ���� ���� ��� ������ ��������� �������
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
