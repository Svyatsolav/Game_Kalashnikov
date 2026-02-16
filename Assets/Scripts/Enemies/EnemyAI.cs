using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [Header("Компоненты")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    
    [Header("Параметры движения")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private float obstacleCheckDistance = 1f;
    [SerializeField] private float raycastHeight = 0.5f;
    
    [Header("Зона видимости")]
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float fieldOfView = 60f;
    [SerializeField] private float heightDifference = 5f;
    
    [Header("Патрулирование (опционально)")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed = 2f;
    
    [SerializeField] Animator anim;
    [SerializeField] private Vector3 offset;
    [SerializeField] Slider hpBar;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool isPlayerDetected = false;
    private bool isGrounded;
    private int currentPatrolIndex = 0;
    private bool facingRight = true;
    private float detectionTimer = 0f;
    private const float DETECTION_DELAY = 0.2f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
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
            hpBar.gameObject.SetActive(health < maxHealth);

            CheckGround();
            CheckPlayerDetection();
            
            if (isPlayerDetected)
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
            }
            
            UpdateFacingDirection();
        }

        hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        hpBar.value = health;
        hpBar.maxValue = maxHealth;
    }
    
    void CheckPlayerDetection()
    {
        if (player == null) return;
        
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float angleToPlayer = Vector2.Angle(transform.up, directionToPlayer);
        
        bool inSightRange = distanceToPlayer <= detectionRange;
        bool inFieldOfView = angleToPlayer <= fieldOfView / 2;
        bool inHeightRange = Mathf.Abs(player.position.y - transform.position.y) <= heightDifference;
        
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position + Vector3.up * raycastHeight,
            directionToPlayer,
            detectionRange,
            groundLayer | obstacleLayer
        );
        
        bool directLineOfSight = hit.collider == null || hit.collider.transform == player;
        
        if (inSightRange && inFieldOfView && inHeightRange && directLineOfSight)
        {
            detectionTimer = DETECTION_DELAY;
            isPlayerDetected = true;
        }
        else if (detectionTimer > 0)
        {
            detectionTimer -= Time.deltaTime;
        }
        else
        {
            isPlayerDetected = false;
        }

        Debug.DrawRay(transform.position + Vector3.up * raycastHeight, 
                     directionToPlayer * detectionRange, 
                     directLineOfSight ? Color.green : Color.red);
    }
    
    void ChasePlayer()
    {
        if (player == null) return;
        
        Vector2 direction = (player.position - transform.position).normalized;
        
        Vector2 checkPosition = new Vector2(transform.position.x, transform.position.y + raycastHeight);
        RaycastHit2D obstacleHit = Physics2D.Raycast(
            checkPosition, 
            new Vector2(direction.x, 0), 
            obstacleCheckDistance, 
            obstacleLayer
        );
        
        bool hasGroundAhead = Physics2D.Raycast(
            new Vector2(transform.position.x + direction.x * 0.5f, transform.position.y),
            Vector2.down,
            groundCheckDistance * 2,
            groundLayer
        );
        
        if (obstacleHit.collider != null && !hasGroundAhead)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        
        float moveDirection = Mathf.Sign(direction.x);
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
    
    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector2 direction = (targetPoint.position - transform.position).normalized;
        
        rb.velocity = new Vector2(direction.x * patrolSpeed, rb.velocity.y);
        
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
        
        Vector2 checkPosition = new Vector2(transform.position.x, transform.position.y + raycastHeight);
        RaycastHit2D obstacleHit = Physics2D.Raycast(
            checkPosition, 
            new Vector2(direction.x, 0), 
            obstacleCheckDistance, 
            obstacleLayer
        );
        
        bool hasGroundAhead = Physics2D.Raycast(
            new Vector2(transform.position.x + direction.x * 0.5f, transform.position.y),
            Vector2.down,
            groundCheckDistance * 2,
            groundLayer
        );
        
        if ((obstacleHit.collider != null || !hasGroundAhead) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    
    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, 
            Vector2.down, 
            groundCheckDistance, 
            groundLayer
        );
        isGrounded = hit.collider != null;
    }
    
    void UpdateFacingDirection()
    {
        float moveDirection = rb.velocity.x;
        
        if (moveDirection > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0 && facingRight)
        {
            Flip();
        }
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Vector3 forward = transform.up;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, fieldOfView / 2) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, -fieldOfView / 2) * forward;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up * raycastHeight, leftBoundary * detectionRange);
        Gizmos.DrawRay(transform.position + Vector3.up * raycastHeight, rightBoundary * detectionRange);
        
        Gizmos.color = Color.red;
        Vector2 direction = player != null ? (player.position - transform.position).normalized : Vector2.right;
        Gizmos.DrawRay(transform.position + Vector3.up * raycastHeight, new Vector2(direction.x, 0) * obstacleCheckDistance);

        if (player != null)
        {
            float moveDir = Mathf.Sign(player.position.x - transform.position.x);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(new Vector2(transform.position.x + moveDir * 0.5f, transform.position.y), 
                          Vector2.down * groundCheckDistance * 2);
        }
    }
}