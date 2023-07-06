using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDFD {
    /// <summary>
    /// ћини игра про закрытие окон Unity
    /// </summary>
    public class UnityMiniGame : MiniGame
    {
        [Header("Setting WindowsUnity MiniGame")]
        public GameObject windowPrefab;
        public Sprite[] windowSprites;
        public GameObject[] windowsPrefabs;
        public int numberOfWindows;
        public int minScale, maxScale;

        private Vector2 minSize;
        private int windowsLeft;

        private float xMin = 0, xMax = 0, yMin = 0, yMax = 0;

       
        // –андомизируетс€ размер окна в пределах размеров диспле€ пк на фоне
        public override void BeginMiniGame()
        {
            base.BeginMiniGame();

            windowsLeft = numberOfWindows;

            RectTransform rectTransform = windowPrefab.GetComponent<RectTransform>();//вот здесь 
            RectTransform selfRect = transform.GetChild(1).GetComponent<RectTransform>();

            minSize = rectTransform.sizeDelta;

            for(int i = 0; i < numberOfWindows; i++)
            {

                RectTransform rect = Instantiate(windowsPrefabs[Random.Range(0, windowsPrefabs.Length)], selfRect).GetComponent<RectTransform>();
               // RectTransform rect = Instantiate(windowPrefab, selfRect).GetComponent<RectTransform>();
                float randSizeScale = Random.Range(minScale, maxScale);//случайный размер 
                rect.sizeDelta = minSize * randSizeScale;
                xMin = selfRect.rect.xMin + rect.rect.width / 2;
                xMax = selfRect.rect.xMax - rect.rect.width / 2;
                yMin = selfRect.rect.yMin + rect.rect.height / 2;
                yMax = selfRect.rect.yMax - rect.rect.height / 2;
                rect.anchoredPosition = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
                rect.GetComponent<Button>().onClick.AddListener(OnWindowClicked);//подписка , на то что когда будет нажата кнопка-окно , то зачисл€тьс€ очки 
               // rect.GetComponent<Image>().sprite = windowSprites[Random.Range(0, windowSprites.Length)];//присвоение случайного спрайта 
            }
            
        }

        public void OnWindowClicked()
        {
            windowsLeft--;
            
            if(windowsLeft <= 0)
            {
                MiniGameEnded();
            }
        }
    }
}
