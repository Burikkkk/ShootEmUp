using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    private bool isPoisoned = false;
    private float poisonDamageInterval = 1f;
    private float nextPoisonDamageTime = 0f;
    public int score = 0; // Текущий счет игрока
    public TextMeshProUGUI scoreText;
    [SerializeField] private Score currentScore= null;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        UpdateScoreUI();
    }

    void Update()
    {
        // Обработка яда
        if (isPoisoned && Time.time >= nextPoisonDamageTime)
        {
            TakeDamage(1);
            nextPoisonDamageTime = Time.time + poisonDamageInterval;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
        FindObjectOfType<HPBar>().UpdateHPBar(); // Обновляем HP бар
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StopPoison(); // Останавливаем яд при получении лечения
        UpdateHealthUI();
        FindObjectOfType<HPBar>().UpdateHPBar(); // Обновляем HP бар
    }

    public void ApplyPoison()
    {
        isPoisoned = true;
    }

    public void StopPoison()
    {
        isPoisoned = false;
    }

    public void Die()
    {
        
        Debug.Log("Игрок погиб!");
        SceneManager.LoadSceneAsync(2); // Загрузка сцены поражения

    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void LoseScore(int amount)
    {
        score -= amount;
        UpdateScoreUI();
    }

    private void UpdateHealthUI()
    {
        if (healthText != null && currentHealth >= 0)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // Метод для обработки убийства 
    public void OnBossKilled(int bossLevel)
    {
        switch (bossLevel) { 
            case 0: AddScore(10); break;
            case 1: AddScore(30); break;
            case 2: AddScore(60); break;
            case 3: 
                AddScore(90);
                ShowVictoryScreen();
                break;
        }
    }

    // Метод для проверки победы
    private void ShowVictoryScreen()
    {
        currentScore.score.Add(score);
        currentScore.WriteToFile("scoreData.txt");
        Debug.Log("Победа! Все боссы убиты.");
        SceneManager.LoadSceneAsync(3); // Загрузка сцены победы (укажите свой номер сцены)
    }

}
