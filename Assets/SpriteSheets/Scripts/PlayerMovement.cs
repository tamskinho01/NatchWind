using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject GameOverPanel;
    public float Speed;
    public float JumpForce;

    private bool Grounded = false;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float horizontal;
    private float lastShoot;
    private bool isDead = false;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        GameOverPanel.SetActive(false); // Asegura que esté oculto al inicio
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", horizontal != 0.0f);

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.25f)
        {
            Shoot();
            lastShoot = Time.time;
        }

        // Muerte por caída
        if (!isDead && transform.position.y < -10f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Jugador ha muerto");
        isDead = true;
        Animator.SetBool("running", false);
        this.enabled = false; // Desactiva el script para evitar más movimiento
        Rigidbody2D.linearVelocity = Vector2.zero;
        Rigidbody2D.bodyType = RigidbodyType2D.Static;

        GameOverPanel.SetActive(true);
    }

    private void Shoot()
    {
        float scaleX = transform.localScale.x;
        Vector2 direction = (Mathf.Sign(scaleX) == 1) ? Vector2.right : Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + (Vector3)(direction * 1.0f), Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        Grounded = false;
    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(horizontal * Speed, Rigidbody2D.linearVelocity.y); // ✅ CORREGIDO
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
    }
}
