using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    [SerializeField] private int health;
    private SpriteRenderer box;
    [SerializeField] GameObject destroyedBox;
    [SerializeField] Sprite[] states;

    void Start()
    {
        box = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(health <= 0) DestroyBox();
        if(health > 0) box.sprite = states[health - 1];
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void DestroyBox()
    {
        Instantiate(destroyedBox, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
