using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float rotationSpeed = 50f; // Скорость поворота дула
    public float minAngle = -45f; // Минимальный угол поворота (нижний предел)
    public float maxAngle = 45f; // Максимальный угол поворота (верхний предел)
    public GameObject bulletPrefab;  // Префаб пули
    public Transform firePoint;      // Точка, откуда будет вылетать пуля
    public float bulletSpeed = 10f;  // Скорость пули
    public AudioClip shootSound;     // Аудиоклип звука выстрела

    private AudioSource audioSource; // Компонент для воспроизведения звука

    void Start()
    {
        // Инициализируем компонент AudioSource
        audioSource = GetComponent<AudioSource>();

       
    }

    void Update()
    {
        // Поворачиваем пушку к курсору
        AimAtMouse();

        // Запуск пули
        if (Input.GetButtonDown("Fire1")) // Fire1 - стандартная кнопка для стрельбы (левая кнопка мыши)
        {
            Shoot();
        }
    }

    void AimAtMouse()
    {
        // Получаем вертикальный ввод от пользователя (значение от -1 до 1)
        float verticalInput = Input.GetAxis("Vertical");

        // Рассчитываем изменение угла поворота на основе скорости и времени
        float rotationAmount = verticalInput * rotationSpeed * Time.deltaTime;

        // Текущий угол поворота дула (угол в пространстве Z)
        float currentAngle = transform.eulerAngles.z;

        // Преобразуем угол из диапазона [0, 360] в диапазон [-180, 180] для удобства
        if (currentAngle > 180) currentAngle -= 360;

        // Рассчитываем новый угол, добавляя поворот
        float newAngle = Mathf.Clamp(currentAngle + rotationAmount, minAngle, maxAngle);

        // Применяем новый угол поворота к оси Z
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    void Shoot()
    {
        // Создаем пулю в точке firePoint с ротацией, как у дула
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Находим направление от firePoint до позиции курсора мыши
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - firePoint.position).normalized;

        // Применяем направление к пуле
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed; // Задаем скорость пули в нужном направлении
        }

        // Воспроизводим звук выстрела
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
