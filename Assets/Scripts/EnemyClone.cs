using Unity.VisualScripting;
using UnityEngine;

public class EnemyClone : MonoBehaviour
{
    GameObject player_object;
    Rigidbody2D enemyClone;
    public float move_speed;
    public GameObject _projectile;
    float _coolDown;
    bool can_attack = true;


    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
        enemyClone = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        FollowPlayer();
        Shoot();
    }

    void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, player_object.transform.position, move_speed * Time.deltaTime
        );
    }

    void Shoot()
    {
        if (can_attack)
        {
            GameObject projectile_instance = Instantiate(
                    _projectile, transform.position, Quaternion.identity
                );

            Vector2 projectile_direction =
                Camera.main.ScreenToWorldPoint(enemyClone.position) - transform.position;

            projectile_direction.Normalize();
            projectile_instance.GetComponent<Rigidbody2D>().AddForce(
                projectile_direction * 10, ForceMode2D.Impulse
            );

            _coolDown = 0;
            can_attack = false;
        }

        CoolDown();
    }

    void CoolDown()
    {
        if (_coolDown > 3) {
            can_attack = true;
        }
        _coolDown += Time.deltaTime;
    }

    // prototype code for caverinha
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<EntityStatus>().hp -= entityStatus.attack_damage;
    //        entityStatus.hp -= entityStatus.max_hp + 1;
    //    }
    //}
}
