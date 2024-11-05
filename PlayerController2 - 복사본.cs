using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumpCount = 2;
    private bool isDead = false;
    private int jumpCount;

    private bool hasBronzeCoin = false;
    private bool hasSilverCoin = false;
    private bool hasGoldCoin = false;
    private bool clear = false;

    private Collider2D collider2D;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject gameOverPanel;
    public GameObject clearPanel;
    void Awake()
    {
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (clearPanel != null)
        {
            clearPanel.SetActive(false);
        }

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();    
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpCount = maxJumpCount;
    }

    void Update()
    {
        if (!isDead)
        {

            float moveInput = 0f;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveInput = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveInput = 1f;
            }

            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            animator.SetInteger("Run", Mathf.Abs((int)moveInput));


            if (moveInput < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (moveInput > 0)
            {
                spriteRenderer.flipX = false;
            }


            if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount--;


                animator.SetBool("Jump", true);
            }

            if (rb.velocity.y == 0)
            {
                animator.SetBool("Jump", false);
            }
        }
        else if (isDead || clear)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // �¿� �̵� �Ұ�
            return;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.9f)
                {
                    jumpCount = maxJumpCount;
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("Obstacle") && !isDead)
        {
            isDead = true;
            StartCoroutine(HandleDeath());
        }

    }

    private IEnumerator HandleDeath()
    {
        // ��� �������� �������� ����
        rb.velocity = new Vector2(0, jumpForce* 1.8f);
        yield return new WaitForSeconds(1f); // �ణ �� �ִ� �ð�

        collider2D.enabled = false;
        animator.enabled = false;
        

        // ĳ���Ͱ� �ٴ����� �������� ����
        rb.velocity = new Vector2(0, -jumpForce);
        yield return new WaitForSeconds(1.5f); // �������� �ð�

        // ���� ���� �г� ǥ��
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("bronzeCoin"))
        {
            Destroy(collision.gameObject);
            hasBronzeCoin = true;
        }

        else if (collision.gameObject.CompareTag("silverCoin"))
        {
            Destroy(collision.gameObject);
            hasSilverCoin = true;
        }

        else if (collision.gameObject.CompareTag("goldCoin"))
        {
            Destroy(collision.gameObject);
            hasGoldCoin = true;
        }

        if (collision.gameObject.CompareTag("goal"))
        {
            if (hasBronzeCoin && hasSilverCoin && hasGoldCoin)
            {
                clear = true;
                clearPanel.SetActive(true);
            }
        }
    }
}