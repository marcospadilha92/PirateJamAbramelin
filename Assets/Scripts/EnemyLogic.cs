using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootInterval = 2f;
    public float projectileSpeed = 10f;
    private float shootTimer = 0f;

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null && projectilePrefab != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            //set colliders and ignore collision with the shooting enemy
            Collider2D[] enemyColliders = GetComponentsInChildren<Collider2D>();
            Collider2D[] projColliders = projectile.GetComponentsInChildren<Collider2D>();
            foreach (var eCol in enemyColliders)
            {
                foreach (var pCol in projColliders)
                {
                    if (eCol != null && pCol != null)
                        Physics2D.IgnoreCollision(eCol, pCol);
                }
            }

            // give motion to the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.linearVelocity = direction * projectileSpeed;
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
