using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header(">>>HEALTH<<<")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] Text hp_text;
    [SerializeField] Slider hp_bar;

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

    [Header(">>>DEATH<<<")]
    [SerializeField] bool isDead = false;
    [SerializeField] GameObject deathPan;
    [SerializeField] float gameTime;
    [SerializeField] Text TimerText;
    [SerializeField] Text gameTimeText;

    [Header(">>>OTHER<<<")]
    [SerializeField] GameObject gun;
    private Animator anim;
    [SerializeField] GameObject damageEffect;
    [SerializeField] GameObject heart;
    [SerializeField] Text moneyText;
    [SerializeField] Text mangoCountText;
    [SerializeField] GameObject mangoTxt;
    [SerializeField] GameObject[] skins;
    [SerializeField] Animator[] anims;
    public static int money = 100;
    public static int mangoCount;
    public static bool isTntPickedUp;
    public int gameCompleteCount;

    private void Start()
    {
        Time.timeScale = 1f;
        isDead = false;
        deathPan.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        for(int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
        }
        skins[ShopScript.currentSkin].SetActive(true);
        anim = anims[ShopScript.currentSkin];
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
        if(health <= 0)
        {
            Death();
        }

        hp_text.text = $"{health}/{maxHealth}";

        hp_bar.value = health;

        mangoCountText.text = mangoCount.ToString();

        if(mangoCount > 0) mangoTxt.SetActive(true);
        else mangoTxt.SetActive(false);

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if(moveInput != 0) anim.SetBool("isWalking", true);
        else if(moveInput == 0) anim.SetBool("isWalking", false);
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space)) rb.velocity = Vector2.up * jumpForce;

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            LogScript.instance.LogPanelActive();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(mangoCount > 0)
            {
                if(health < maxHealth)
                {
                    mangoCount--;
                    AddHealth(25);
                }
                else
                {
                    //Пошел нафиг нельзя))
                }
            }
        }

        if(isDead == false)
        {
            gameTime += Time.deltaTime;
            UpdateTimerUI();
        }

        moneyText.text = money.ToString();
        PlayerPrefs.SetInt("money", money);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Minecart"))
        {
            rb.mass = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Minecart"))
        {
            rb.mass = 1;
        }
    }
    void UpdateTimerUI()
    {
        string minutes = Mathf.Floor(gameTime / 60).ToString("00");
        string seconds = Mathf.Floor(gameTime % 60).ToString("00");
        string milli = Mathf.Floor((gameTime * 100) % 100).ToString("00");

        TimerText.text = minutes + ":" + seconds + ":" + milli;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(damageEffect, gameObject.transform);
    }
    public void AddHealth(int hp)
    {
        if(health + hp <= maxHealth) health += hp;
        else health = maxHealth;
    }
    public void DeathPan()
    {
        if(isDead == false)
        {
            isDead = true;
            deathPan.SetActive(true);
            Time.timeScale = 0f;
            gameTimeText.text = "Время забега: " + TimerText.text;
        }
    }
    public void Death()
    {
        DeathPan();
    }
    public void RestartGame()
    {
        deathPan.SetActive(false);
        isDead = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void CompleteGame()
    {
        gameCompleteCount++;
        PlayerPrefs.SetInt("CompleteCount", gameCompleteCount);
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}