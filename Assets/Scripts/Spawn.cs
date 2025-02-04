using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Массив префабов врагов (базовый враг и боссы)
    public Transform[] spawnPoints; // Массив точек спавна врагов (справа и другие)
    public float initialSpawnDelay = 0.5f; // Задержка перед первым спавном
    public float basicEnemySpawnInterval = 2f; // Интервал спавна базовых врагов
    private float timeToSpawn; // Таймер для спавна врагов
    private float bossSpawnTime; // Таймер для спавна боссов
    private int bossCounter = 0; // Счетчик боссов
    public GameObject heartPrefab;

    [SerializeField] private LevelGraphics[] levels;
    [SerializeField] private AudioClip[] levelAudioClips;

    void Start()
    {
        // Установка времени спавна базовых врагов
        timeToSpawn = Time.time + initialSpawnDelay;
        bossSpawnTime = Time.time + 5f; // Первый босс появится через 30 секунд
    }

    void Update()
    {
        // Логика спавна базовых врагов
        if (Time.time >= timeToSpawn)
        {
            SpawnBasicEnemy(); // Спавним базового врага
            timeToSpawn = Time.time + basicEnemySpawnInterval; // Устанавливаем следующий спавн базового врага
        }

        // Логика спавна боссов
        if (bossCounter < 3 && Time.time >= bossSpawnTime)
        {
            SpawnBoss();
            bossSpawnTime += GetNextBossSpawnInterval(); // Устанавливаем время для следующего босса
        }
    }

    void SpawnBasicEnemy()
    {
        // Выбираем случайную точку спавна
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Создаем базового врага в случайной точке спавна
        Instantiate(enemyPrefabs[0], randomSpawnPoint.position, Quaternion.identity);
    }

    void SpawnBoss()
    {
        GameObject bossPrefab = enemyPrefabs[bossCounter + 1]; // Получаем префаб для текущего босса
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        var newBoss = Instantiate(bossPrefab, randomSpawnPoint.position, Quaternion.identity);
        LevelGraphics nextLevel;
        AudioClip nextClip;
        if (bossCounter + 1 >= levels.Length)
        {
            nextLevel = null;
            nextClip = null;
        }
        else
        {
            nextLevel = levels[bossCounter + 1];
            nextClip = levelAudioClips[bossCounter + 1];
        }
        
        newBoss.GetComponent<Boss>().SetLevel(levels[bossCounter], nextLevel, nextClip);
        bossCounter++; // Увеличиваем счетчик боссов
    }

    public void SpawnHeart()
    {
        if (heartPrefab != null)
        {
            // Выбираем случайную точку спавна
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(heartPrefab, randomSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Сердечко не назначено в инспекторе!");
        }
    }

    float GetNextBossSpawnInterval()
    {
        switch (bossCounter)
        {
            case 1: return 5f; // Второй босс через 60 секунд
            case 2: return 10f; // Третий босс через 90 секунд
            default: return 0f; // Это никогда не произойдет, так как bossCounter не может быть больше 2
        }
    }
}
