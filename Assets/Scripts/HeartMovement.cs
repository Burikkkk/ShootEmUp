using UnityEngine;

public class HeartMovement : MonoBehaviour
{
    public float speed = 3f; // Скорость движения сердечка

    void Update()
    {
        // Движение сердечка влево
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Уничтожение сердечка при соприкосновении с границей
        if (collision.CompareTag("Boundary")) 
        {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Destroy(gameObject); 
    }
}
