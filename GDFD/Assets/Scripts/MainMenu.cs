using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDFD
{
    /// <summary>
    /// Скрипт главного меню. Не написан
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
