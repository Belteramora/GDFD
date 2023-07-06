using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDFD
{
    /// <summary>
    /// Мини-игра про звонок, на который не нужно отвечать
    /// </summary>
    public class PhoneMiniGame : MiniGame
    {
        [Header("Setting Phone MiniGame")]
        public bool isPhoneTaked;
        public int scoreTake=0; // 0 - максимальный, 9 - минимальный 

        public override void MiniGameEnded()
        {
            // Так как тут наоборот, нужно подождать пока время кончится
            // то если на звонок ответили, время ставится на минимум и игра считается проигранной
            // если не ответили, то время ставится на максимум и игрок получается максимум очков
            if (!isPhoneTaked)
                currentTime = scoreTake;
            else
                currentTime = timeForMiniGame;

            base.MiniGameEnded();
        }

        /// <summary>
        /// Функция привязывается к телефону в миниигре. При нажатии звонок считается отвеченным
        /// </summary>
        public void OnPhoneTake()
        {
            isPhoneTaked = true;

            MiniGameEnded();
        }
    }
}
