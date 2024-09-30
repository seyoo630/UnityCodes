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

        // 캐릭터의 방향에 따라 스프라이트 반전
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true; // 왼쪽으로 이동할 때 스프라이트 반전
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // 오른쪽으로 이동할 때 기본 상태로 설정
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount--;

            // 점프 애니메이션 트리거 설정
            animator.SetBool("Jump", true);
        }

        // 점프 애니메이션 해제: 플레이어가 수직 속도가 0일 때
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