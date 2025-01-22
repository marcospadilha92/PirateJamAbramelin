using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;

        if (other.tag == "Enemy")
        {
            Debug.Log("Player collided with enemy");
            other.GetComponent<EnemyLogic>().Die();
        }
    }
}
