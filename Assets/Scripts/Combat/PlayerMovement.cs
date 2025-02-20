using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashDuration;
    Vector2 direction = Vector2.zero;
    Vector2 lastDirection = Vector2.zero;
    float dashTimer;
    Vector3 left = new Vector3(-1, 1, 1);
    Rigidbody2D body;
    CapsuleCollider2D collider;
    bool isDashing = false;
    Animator animator;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        
        if (direction != Vector2.zero &&
            !isDashing)
        {
            lastDirection = direction;
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        Dash();
        LookAtDirection();
    }

    void FixedUpdate()
    {
        if(isDashing)
        {
            body.linearVelocity = lastDirection.normalized * dashSpeed;
        }
        else
        {
            body.linearVelocity = direction.normalized * moveSpeed;
        }         
    }

    void LookAtDirection()
    {
        if (direction != lastDirection) return;
        if (lastDirection.x >= 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = left;
        }
    }

    void Dash()
    {
        dashTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) &&
           dashTimer > dashCooldown)
        { 
            animator.SetTrigger("Dash");
            dashTimer = 0;
            isDashing = true;
            collider.enabled = false;
            Invoke("StopDash", dashDuration);          
        }    
    }

    void StopDash()
    {
        isDashing = false;
        collider.enabled = true;
    }
}