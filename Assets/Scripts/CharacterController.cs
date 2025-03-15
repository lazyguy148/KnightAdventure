
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float horizontal;
    private float speed = 2f;
    private float jumpPower = 8f;
    private bool isFacingRight = true;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private float attackCooldown = 0.5f; 
    private float lastAttackTime = 0f;


    public float attackRange = 0.22f;
    public LayerMask enemyLayers;

    public ParticleSystem dust;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource attackSound;

    [System.Obsolete]
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            dust.Play();
            jumpSound.Play();
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if(coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpBufferCounter = 0f;
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
        // Chỉ tấn công nếu đã đủ thời gian chờ từ lần tấn công cuối
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            attackSound.Play();
            Attack();
            lastAttackTime = Time.time; // Cập nhật thời gian tấn công cuối
        }
        Flip();
        animator.SetBool("run", horizontal != 0f);
        animator.SetBool("grounded", isGrounded());
    }


    [System.Obsolete]
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    [System.Obsolete]
    private void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy1>().TakeDamage(30);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            if (isGrounded())
            {
                dust.Play();
            }
        }
        
    }

}
