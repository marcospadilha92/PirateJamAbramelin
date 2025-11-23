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
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
