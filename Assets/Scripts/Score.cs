using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Score : MonoBehaviour
{
    public List<int> score;

    // Start is called before the first frame update
    void Start()
    {        

        //WriteToFile("scoreData.txt", score); // Запись данных в файл
       score = ReadFromFile("scoreData.txt"); // Чтение данных из файла

        if (score == null)
            score = new List<int>();
        DisplayScore();

    }

    public void WriteToFile(string fileName)
    {
        // Получаем путь к файлу в папке постоянных данных
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // Преобразуем список чисел в массив строк
        string[] lines = score.Select(number => number.ToString()).ToArray();

        // Записываем содержимое в файл
        File.WriteAllLines(path, lines);

        Debug.Log($"Data written to file at {path}");
    }

    public List<int> ReadFromFile(string fileName)
    {
        // Получаем путь к файлу
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // Проверяем, существует ли файл
        if (File.Exists(path))
        {
            // Читаем строки из файла в массив
            string[] lines = File.ReadAllLines(path);

            // Преобразуем каждую строку в число и создаем список
            List<int> scores = lines.Select(line => int.Parse(line)).ToList();

            Debug.Log("Data read from file: " + string.Join(", ", scores));
            return scores;
        }
        else
        {
            Debug.LogWarning("File not found at path: " + path);
            return null;
        }
    }


    public string DisplayScore()
    {
        // Сортируем список по убыванию
        score.Sort((a, b) => b.CompareTo(a));

        // Берем топ 10 значений (или меньше, если в списке меньше 10 элементов)
        List<int> topScores = score.Take(10).ToList();

        string result = string.Join("\n", topScores);

        Debug.Log("Top 10 Scores from file:");
        Debug.Log(result);

        return result;
    }

}
