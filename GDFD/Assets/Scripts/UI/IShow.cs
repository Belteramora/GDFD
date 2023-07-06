using System.Collections;
using UnityEngine;

namespace GDFD
{
    /// <summary>
    /// Интерфейс для окон вывода инфы
    /// </summary>
    public interface IShow
    {
        
        public void Show(ShowInfo info);
    }

    // Структура, нужная для хранения инфы для вывода
    public struct ShowInfo
    {
        public int score;
        public int numberOfLifes;
    }
}