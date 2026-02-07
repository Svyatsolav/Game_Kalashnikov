using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if(health <= 0) Destroy(gameObject);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
