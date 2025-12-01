using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 10f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerLogic player = collision.collider.GetComponent<PlayerLogic>();
            if (player != null)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null && rb.linearVelocity.magnitude > projectileSpeed * 0.5f)
                {
                    player.Die();
                }
            }
            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyLogic enemy = collision.collider.GetComponent<EnemyLogic>();
            if (enemy != null)
            {
                enemy.Die();
            }
            Destroy(gameObject);
        }
    }
}