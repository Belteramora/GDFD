using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDFD
{
    /// <summary>
    /// Скрипт цикла игры
    /// </summary>
    public class MiniGameCycle : MonoBehaviour
    {
        // Ссылка на пул мини игр и на канвас
        public MiniGamesPool miniGamesPool;
        public Transform canvas;

        // Очередь сгенерированных мини игр
        public ImprovedQueue<MiniGame> currentMiniGames;

        // Нынешняя стадия игры. На его основе выбирается пул для генерации
        public MiniGameDirection currentStage;
        [HideInInspector]
        // Ссылка на состояние игры
        public GameState currentGameState;

        // Пул всех мини игр
        private List<List<GameObject>> allMiniGames = new List<List<GameObject>>();

        private int householdMiniGames;

        public void Start()
        {
            currentMiniGames = new ImprovedQueue<MiniGame>();
            currentGameState = GetComponent<GameState>();

            allMiniGames.Add(miniGamesPool.householdMiniGames);
            allMiniGames.Add(miniGamesPool.firstStageMiniGames);
            allMiniGames.Add(miniGamesPool.secondStageMiniGames);
            allMiniGames.Add(miniGamesPool.thirdStageMiniGames);

            GenerateMiniGameSequence();
        }

        // Функция генерирующая мини игры в очередь
        public void GenerateMiniGameSequence()
        {
            for(int i = 0; i <= 20; i++)
            {
                GenerateRandomMiniGame(0);
            }

            // Если процент бытовых мини игр больше 20 то убрать бытовые мини игры и заменить их на другие 
            if (householdMiniGames > currentMiniGames.Count * 0.2f)
            {
                List<MiniGame> tempList = currentMiniGames.FindAll(x => x.miniGameStage == MiniGameDirection.Household);
                for (int i = 0; householdMiniGames < currentMiniGames.Count * 0.02f; householdMiniGames--, i++)
                {
                    currentMiniGames.Remove(tempList[i]);

                    GenerateRandomMiniGame(1);
                }
            }

            // Переход к первой мини игре в очереди
            NextMiniGame();
        }

        // Генерация одной мини игры. В параметры функции передается нижний порог рандома 
        public void GenerateRandomMiniGame(int randMinPool)
        {
            int randomPool = Random.Range(randMinPool, (int)currentStage);
            int randomMiniGame = Random.Range(0, allMiniGames[randomPool].Count);

            if (randomPool == 0)
                householdMiniGames++;

            // Инициализация мини игры
            InstantiateMiniGame(allMiniGames[randomPool][randomMiniGame]);
        }

        // Инициализация мини игры, в качестве параметра принимает префаб мини игры
        // Потом происходит спавн мини игры, привязка конца мини игры к другой функции
        // Инстанцированная мини игра кидается в очередь, и выключается до момента ее выбора
        public void InstantiateMiniGame(GameObject miniGamePrefab)
        {
            GameObject miniGameObject = Instantiate(miniGamePrefab, canvas);
            miniGameObject.SetActive(false);

            MiniGame miniGameScript = miniGameObject.GetComponent<MiniGame>();
            miniGameScript.onMiniGameEnded.AddListener(currentGameState.OnMiniGameEnded);
            currentMiniGames.Enqueue(miniGameScript);
        }

        // Функция выбора следующей мини игры. Она просто забирается из очереди
        public void NextMiniGame()
        {
            Debug.Log("Start");
            MiniGame miniGame = currentMiniGames.Dequeue();
            miniGame.gameObject.SetActive(true);
            miniGame.BeginMiniGame();
        }
    }

    // Перечисление стадии игры
    public enum MiniGameDirection
    {
        Household = 1,
        FirstStage = 2,
        SecondStage = 3,
        ThirdStage = 4
    }

    // Переорпедление обычного списка на своеобразную очередь. Чтобы можно было доставать любой элемент очереди и при это функционал очереди сохранялся
    [System.Serializable]
    public class ImprovedQueue<T>: List<T>
    {
        public void Enqueue(T obj)
        {
            Add(obj);
        }

        public T Dequeue()
        {
            T obj = this[0];
            RemoveAt(0);

            return obj;
        }
    }
}
