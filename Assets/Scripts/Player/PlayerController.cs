using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header(">>>HEALTH<<<")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] Text hp_text;

    [Header(">>>MOVEMENT<<<")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    [SerializeField] Transform feetPos;
    [SerializeField] private float checkRadius;
    [SerializeField] LayerMask whatIsGround;

    [Header(">>>OTHER<<<")]
    [SerializeField] GameObject gun;
    [SerializeField] Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if(facingRight == false && moveInput > 0) Flip();
        else if(facingRight == true && moveInput < 0) Flip();
    }

    private void Update()
    {
        hp_text.text = $"HP: {health}/{maxHealth}";

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if(moveInput != 0) anim.SetBool("isWalking", true);
        else if(moveInput == 0) anim.SetBool("isWalking", false);
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space)) rb.velocity = Vector2.up * jumpForce;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    
}
