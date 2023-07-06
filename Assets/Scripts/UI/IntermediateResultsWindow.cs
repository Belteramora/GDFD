using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GDFD
{
    /// <summary>
    /// Окно промежуточного результата
    /// </summary>
    public class IntermediateResultsWindow : MonoBehaviour, IShow
    {
        public TextMeshProUGUI score;//текс для очков
        public CanvasGroup canvasGroup;//общитй контейнер

        public Image[] images; //иконки жизней

        public UnityEvent onShowInfo;//эвент котрый будет после закрытия окна 

        // Функция показа, наследутеся от интерфейса
        public void Show(ShowInfo info)
        {
            SetWindowActive(true);

            StartCoroutine(ShowRes(info));
        }

        // Корутин на задержку показа окна на две секунды, время можно задать плавающим через параметр
        public IEnumerator ShowRes(ShowInfo info)
        {
            score.text = "Score: " + info.score;

            for (int i = 0; i < images.Length; i++)
            {
                if(i < info.numberOfLifes)
                    images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1);
                else
                    images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 0);
            }

            yield return new WaitForSeconds(2f);

            SetWindowActive(false);

            onShowInfo.Invoke();
        }

        // Включение и выключение окна
        public void SetWindowActive(bool isActive)
        {
            if (isActive)
            {
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }
        }

    }
}