using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(damage(other));
        }
    }
    IEnumerator damage(Collider2D other)
    {
        other.GetComponent<PlayerController>().TakeDamage(1);
        yield return new WaitForSeconds(1);
    }
}
