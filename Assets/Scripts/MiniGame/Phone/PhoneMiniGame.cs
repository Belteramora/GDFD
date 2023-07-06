using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDFD
{
    /// <summary>
    /// ����-���� ��� ������, �� ������� �� ����� ��������
    /// </summary>
    public class PhoneMiniGame : MiniGame
    {
        [Header("Setting Phone MiniGame")]
        public bool isPhoneTaked;
        public int scoreTake=0; // 0 - ������������, 9 - ����������� 

        public override void MiniGameEnded()
        {
            // ��� ��� ��� ��������, ����� ��������� ���� ����� ��������
            // �� ���� �� ������ ��������, ����� �������� �� ������� � ���� ��������� �����������
            // ���� �� ��������, �� ����� �������� �� �������� � ����� ���������� �������� �����
            if (!isPhoneTaked)
                currentTime = scoreTake;
            else
                currentTime = timeForMiniGame;

            base.MiniGameEnded();
        }

        /// <summary>
        /// ������� ������������� � �������� � ��������. ��� ������� ������ ��������� ����������
        /// </summary>
        public void OnPhoneTake()
        {
            isPhoneTaked = true;

            MiniGameEnded();
        }
    }
}
