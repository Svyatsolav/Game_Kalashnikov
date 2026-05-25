using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public Animator anim;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            EnemyAI enemy = gameObject.GetComponentInParent<EnemyAI>();
            if(enemy.dead == false) StartCoroutine(damage(other));
            else StopCoroutine(damage(other));
        }
    }
    IEnumerator damage(Collider2D other)
    {
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        other.GetComponent<PlayerController>().TakeDamage(1);
    }
}
