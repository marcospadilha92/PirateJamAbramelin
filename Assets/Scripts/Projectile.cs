using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerLogic player = collision.collider.GetComponent<PlayerLogic>();
            if (player != null)
            {
                player.Die();
            }
            Destroy(gameObject); 
        }
    }
}