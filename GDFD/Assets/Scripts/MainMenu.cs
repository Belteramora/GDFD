using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDFD
{
    /// <summary>
    /// ������ �������� ����. �� �������
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
