using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 offset;
    [SerializeField] Slider hpBar;
    [SerializeField] Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if(health <= 0)
        {
            anim.SetTrigger("death");
            bc.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            hpBar.gameObject.SetActive(false);
        }
        else
        {
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            if(player.transform.position.x > transform.position.x) transform.eulerAngles = new Vector3(0, 0, 0);
            else transform.eulerAngles = new Vector3(0, 180, 0);
            hpBar.gameObject.SetActive(health < maxHealth);
        }

        hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        hpBar.value = health;
        hpBar.maxValue = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void Knockback(Vector2 direction, float force)
    {
        if (rb == null) return;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
    public void Stop()
    {
        
    }
}
