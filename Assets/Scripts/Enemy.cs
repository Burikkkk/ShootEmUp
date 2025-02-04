using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // �������� �����
    private Transform target; // ����, � ������� �������� ���� (�����)
    public int health = 3; // �������� �����
    public GameObject bloodSplatterPrefab; // ������ ������ �����
    public int damage = 5; // ���� �����
    public int bossLevel = 0; // �������� �� ���� ������

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // ����� ������ �� ����
    }

    void Update()
    {
        if (target != null)
        {
            // ����������� �������� � ������
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime; // �������� � ������
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ���������� �������� �����
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (bossLevel > 0)
        {
            // ��������� ������ �����
            SpawnBloodSplatter();
        }
        

        Destroy(gameObject); // ���������� �����
    }

    void SpawnBloodSplatter()
    {
        if (bloodSplatterPrefab != null)
        {
            // ����� ������� ������� ������ � ������������ � ���
            var splatter = Instantiate(bloodSplatterPrefab, bloodSplatterPrefab.transform.position, Quaternion.identity);
            Vector3 splatterPosition = splatter.transform.position;
            splatterPosition.x += Camera.main.transform.position.x;
            splatter.transform.position = splatterPosition;
            splatter.transform.parent = Camera.main.transform;
        }
    }


    // ����� ��� ��������� ������������ � �������
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            Spawn heartSpawner = FindObjectOfType<Spawn>(); // ����� ��������� Spawn

            if (playerHealth != null)
            {
                if (bossLevel>0)
                {
                    playerHealth.Die(); // ���� ���� - ����, ����� �������
                }
                else
                {
                    playerHealth.TakeDamage(damage); // ������� ���� ������
                    playerHealth.ApplyPoison(); // ����������� ������ ����������
                    heartSpawner.SpawnHeart(); // ������� ��������
                }
                Die(); // ���������� ����� ����� �����
            }
        }
    }

}
