using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDFD
{
    /// <summary>
    /// Простой скрипт для хранения мини игр по стадиям
    /// </summary>
    [CreateAssetMenu(fileName = "MiniGamesPool", menuName = "MiniGame/MiniGamesPool")]
    public class MiniGamesPool : ScriptableObject
    {
        public List<GameObject> householdMiniGames;
        public List<GameObject> firstStageMiniGames;
        public List<GameObject> secondStageMiniGames;
        public List<GameObject> thirdStageMiniGames;
    }
}