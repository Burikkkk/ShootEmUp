using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GameObject[] healthPrefabs; // Массив префабов изображений для HP
    private GameObject currentHealthImage; // Текущий префаб изображения для HP

    private PlayerHealth playerHealth; // Ссылка на PlayerHealth для отслеживания здоровья

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>(); // Получаем ссылку на PlayerHealth
        UpdateHPBar(); // Инициализируем HP бар
    }

    void Update()
    {
        UpdateHPBar(); // Обновляем HP бар в каждом кадре
    }

    public void UpdateHPBar()
    {
        int currentHealth = playerHealth.currentHealth; // Получаем текущее здоровье
        int maxHealth = playerHealth.maxHealth; // Максимальное здоровье

        // Уничтожаем предыдущий префаб изображения (если есть)
        if (currentHealthImage != null)
        {
            Destroy(currentHealthImage);
        }
        int index = Mathf.FloorToInt((currentHealth / (float)maxHealth) * healthPrefabs.Length);
        index = Mathf.Clamp(index, 0, healthPrefabs.Length - 1); // Убедитесь, что индекс не выходит за границы

        // Создаем новый префаб изображения
        Vector3 newPosition = healthPrefabs[index].transform.position;
        newPosition.x += Camera.main.transform.position.x;
        currentHealthImage = Instantiate(healthPrefabs[index], newPosition , Quaternion.identity, transform);
    }
}
