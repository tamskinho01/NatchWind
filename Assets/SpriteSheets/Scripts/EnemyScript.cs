using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject Player;
    public float DetectionRange = 5f;
    public float AttackRange = 1.3f;
    public float Speed = 2f;
    public float AttackCooldown = 1.0f;
    public int Health = 3;

    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private Collider2D Collider2D;
    private float lastAttackTime = 0f;
    private bool isDead = false;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isDead || Player == null) return;

        Vector3 direction = Player.transform.position - transform.position;

        // Mirar al jugador
        if (direction.x >= 0.0f)
            transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
        else
            transform.localScale = new Vector3(-0.4f, 0.4f, 1.0f);

        float distance = Vector2.Distance(transform.position, Player.transform.position);
        bool seesPlayer = distance <= DetectionRange;
        Animator.SetBool("seesPlayer", seesPlayer);

        // Movimiento
        if (seesPlayer && distance > AttackRange)
        {
            Vector2 moveDir = direction.normalized;
            Rigidbody2D.linearVelocity = new Vector2(moveDir.x * Speed, Rigidbody2D.linearVelocity.y);
        }
        else
        {
            Rigidbody2D.linearVelocity = new Vector2(0, Rigidbody2D.linearVelocity.y);
        }

        // Ataque
        if (distance <= AttackRange && Time.time > lastAttackTime + AttackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        Health -= amount;

        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Animator.SetTrigger("Die");

        // Detener movimiento y físicas
        Rigidbody2D.linearVelocity = Vector2.zero;
        Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

        // Desactivar colisiones
        if (Collider2D != null)
            Collider2D.enabled = false;

        // Destruir después de 1 segundo (tiempo de animación de muerte)
        Destroy(gameObject, 1.0f);
    }

    void Attack()
    {
        Animator.SetTrigger("isAttacking");
        Debug.Log("¡Zombie ataca al jugador!");
    }
}
