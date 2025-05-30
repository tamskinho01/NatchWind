using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed = 10f;
    private Vector2 Direction = Vector2.right;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Inicializamos la velocidad una vez en Start si la dirección ya está seteada
        rb.linearVelocity = Direction * Speed;

        // Destruir la bala después de 5 segundos para limpiar la escena si no colisiona
        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        // Mover la bala en la dirección dada
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
