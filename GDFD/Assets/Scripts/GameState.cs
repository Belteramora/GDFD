using System.Collections;
using UnityEngine;

namespace GDFD
{
    /// <summary>
    /// Скрипт состояния игры
    /// </summary>
    public class GameState : MonoBehaviour
    {
          
        public int maxPossibleTries;// Максимальное количество жизней
        public int possibleTriesLeft;//нынешнее количество жизней
        public int currentScore;//нынешний счет

        // Хранение окна результата по интерфейсу. Можешь переделать
        private IShow resultWindow;//????

        
        public IntermediateResultsWindow intermediateResultsWindow;//промежуточный результат 
        public FinalResultsWindow finalResultsWindow;//финальный счет

        public void Start()
        {
            possibleTriesLeft = maxPossibleTries;

            resultWindow = intermediateResultsWindow;//????
        }

        // Функция, вызывающаяся при окончании мини игры. В качестве параметров передается счет за мини игру
        public void OnMiniGameEnded(int score)
        {
            // Он прибавляется к общему
            currentScore += score;

            Debug.Log(score);

            // Если количество очков за игру меньше нуля, то минус жизнь
            // Если больше нуля, то в качестве окна вывода ставится окно промежуточного результата
            if(score <= 0)
            {
                possibleTriesLeft--;

                // Если жизней меньше нуля, то игра проиграна, и в качестве окна вывода ставится финальное окно
                // Если жизней один и больше, то также ставится окно промежуточного результата
                if(possibleTriesLeft <= 0)
                {
                    resultWindow = finalResultsWindow;
                }
            }
            else
            {
                resultWindow = intermediateResultsWindow;
            }

            // Генерируется структура, устанавливаются значения
            ShowInfo info = new ShowInfo { score = currentScore, numberOfLifes = possibleTriesLeft};

            // Структура передается дальше в выбранный скрипт окна результата
            ShowResults(info);
        }
        public void ShowResults(ShowInfo info)//открыть окно результата
        {
            resultWindow.Show(info);
        }
    }
}