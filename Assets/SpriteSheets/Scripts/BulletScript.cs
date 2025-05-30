using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed = 20f;          // Velocidad de la bala
    public float LifeTime = 7f;        // Tiempo que dura la bala antes de destruirse

    private Vector2 Direction = Vector2.right;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Aplica la velocidad inicial
        rb.linearVelocity = Direction * Speed;

        // Destruye la bala tras LifeTime segundos
        Destroy(gameObject, LifeTime);
    }

    void FixedUpdate()
    {
        // Mantiene la velocidad constante
        rb.linearVelocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction.normalized;
        if (rb != null)
        {
            rb.linearVelocity = Direction * Speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
