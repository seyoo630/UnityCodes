using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumpCount = 2;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer; 
    private int jumpCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpCount = maxJumpCount;
    }

    void Update()
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

        // ĳ������ ���⿡ ���� ��������Ʈ ����
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true; // �������� �̵��� �� ��������Ʈ ����
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // ���������� �̵��� �� �⺻ ���·� ����
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount--;

            // ���� �ִϸ��̼� Ʈ���� ����
            animator.SetBool("Jump", true);
        }

        // ���� �ִϸ��̼� ����: �÷��̾ ���� �ӵ��� 0�� ��
        if (rb.velocity.y == 0)
        {
            animator.SetBool("Jump", false);
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
    }
}