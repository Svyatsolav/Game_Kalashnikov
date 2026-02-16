using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapons : MonoBehaviour
{
    private float timeBtwAttack;
    [SerializeField] float startTimeBtwAttack;
    [SerializeField] Transform attackPos;
    [SerializeField] LayerMask enemy;
    [SerializeField] float attackRange;
    [SerializeField] int damage;
    [SerializeField] float knockbackForce;
    [SerializeField] Animator anim;
    private Weapon weapon;

    void Start()
    {
        weapon = GetComponent<Weapon>();
    }
    void Update()
    {
        if(weapon.IsEquipped == true)
        {
            if(timeBtwAttack <= 0)
            {
                if(Input.GetMouseButton(0))
                {
                    anim.SetTrigger("Attack");
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
                    for(int i = 0; i < enemies.Length; i++)
                    {
                        // урон
                        enemies[i].GetComponent<EnemyAI>().TakeDamage(damage);

                        // откидывание
                        Vector2 hitDirection = (enemies[i].transform.position - transform.position).normalized;
                        enemies[i].GetComponent<EnemyAI>().Knockback(hitDirection, knockbackForce);
                    }
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
            else timeBtwAttack -= Time.deltaTime;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
