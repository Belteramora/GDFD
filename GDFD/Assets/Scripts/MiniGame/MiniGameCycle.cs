using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDFD
{
    /// <summary>
    /// ������ ����� ����
    /// </summary>
    public class MiniGameCycle : MonoBehaviour
    {
        // ������ �� ��� ���� ��� � �� ������
        public MiniGamesPool miniGamesPool;
        public Transform canvas;

        // ������� ��������������� ���� ���
        public ImprovedQueue<MiniGame> currentMiniGames;

        // �������� ������ ����. �� ��� ������ ���������� ��� ��� ���������
        public MiniGameDirection currentStage;
        [HideInInspector]
        // ������ �� ��������� ����
        public GameState currentGameState;

        // ��� ���� ���� ���
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

        // ������� ������������ ���� ���� � �������
        public void GenerateMiniGameSequence()
        {
            for(int i = 0; i <= 20; i++)
            {
                GenerateRandomMiniGame(0);
            }

            // ���� ������� ������� ���� ��� ������ 20 �� ������ ������� ���� ���� � �������� �� �� ������ 
            if (householdMiniGames > currentMiniGames.Count * 0.2f)
            {
                List<MiniGame> tempList = currentMiniGames.FindAll(x => x.miniGameStage == MiniGameDirection.Household);
                for (int i = 0; householdMiniGames < currentMiniGames.Count * 0.02f; householdMiniGames--, i++)
                {
                    currentMiniGames.Remove(tempList[i]);

                    GenerateRandomMiniGame(1);
                }
            }

            // ������� � ������ ���� ���� � �������
            NextMiniGame();
        }

        // ��������� ����� ���� ����. � ��������� ������� ���������� ������ ����� ������� 
        public void GenerateRandomMiniGame(int randMinPool)
        {
            int randomPool = Random.Range(randMinPool, (int)currentStage);
            int randomMiniGame = Random.Range(0, allMiniGames[randomPool].Count);

            if (randomPool == 0)
                householdMiniGames++;

            // ������������� ���� ����
            InstantiateMiniGame(allMiniGames[randomPool][randomMiniGame]);
        }

        // ������������� ���� ����, � �������� ��������� ��������� ������ ���� ����
        // ����� ���������� ����� ���� ����, �������� ����� ���� ���� � ������ �������
        // ���������������� ���� ���� �������� � �������, � ����������� �� ������� �� ������
        public void InstantiateMiniGame(GameObject miniGamePrefab)
        {
            GameObject miniGameObject = Instantiate(miniGamePrefab, canvas);
            miniGameObject.SetActive(false);

            MiniGame miniGameScript = miniGameObject.GetComponent<MiniGame>();
            miniGameScript.onMiniGameEnded.AddListener(currentGameState.OnMiniGameEnded);
            currentMiniGames.Enqueue(miniGameScript);
        }

        // ������� ������ ��������� ���� ����. ��� ������ ���������� �� �������
        public void NextMiniGame()
        {
            Debug.Log("Start");
            MiniGame miniGame = currentMiniGames.Dequeue();
            miniGame.gameObject.SetActive(true);
            miniGame.BeginMiniGame();
        }
    }

    // ������������ ������ ����
    public enum MiniGameDirection
    {
        Household = 1,
        FirstStage = 2,
        SecondStage = 3,
        ThirdStage = 4
    }

    // �������������� �������� ������ �� ������������ �������. ����� ����� ���� ��������� ����� ������� ������� � ��� ��� ���������� ������� ����������
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
