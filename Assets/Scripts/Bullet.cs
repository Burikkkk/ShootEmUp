using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Установка скорости пули
    public float lifetime = 2f; // Время жизни пули
    public int damage = 10; // Урон, который наносит пуля
    public LayerMask whatIsSolid; // Слой, с которым пуля может столкнуться

    private void Start()
    {
        // Уничтожаем пулю через определенное время
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Перемещение пули
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Проверка на столкновение с препятствиями
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, speed * Time.deltaTime, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                EnemyMovement enemy = hitInfo.collider.GetComponent<EnemyMovement>(); // Получаем врага, который был поражен
                enemy.TakeDamage(damage); // Наносим урон врагу
                Destroy(gameObject); // Уничтожаем пулю после попадания

                // Теперь мы можем получить уровень босса непосредственно из врага
                int level = enemy.bossLevel;

                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                playerHealth.OnBossKilled(level); // Передаем уровень босса
            }
            else if (hitInfo.collider.CompareTag("Heart"))
            {
                // Получаем ссылку на PlayerHealth
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(10); // Лечим игрока на 10 единиц
                    playerHealth.StopPoison(); // Останавливаем яд при получении сердца
                }

                hitInfo.collider.GetComponent<HeartMovement>().Die();
                Destroy(gameObject); // Уничтожаем пулю после попадания
            }
            else
            {
                Destroy(gameObject); // Уничтожаем пулю при столкновении с любым другим объектом
            }
        }
    }
}
