using System.Collections;
using UnityEngine;

public class SKELETON1 : MonoBehaviour
{
    public Animator animator;
    public GameObject Player;
    public bool flip;
    public float knockbackForce = 1f;

    public float attackRange = 0.4f;
    public float detectionRange = 1.5f;
    public float moveSpeed = 0.75f; // Tốc độ di chuyển khi theo dõi Player

    public LayerMask playerLayers;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform attackPoint;

    private bool playerInSight = false;

    public float attackCooldown = 1.75f; // Thời gian chờ giữa các lần tấn công
    private float lastAttackTime = 0f; // Thời gian của lần tấn công cuối cùng

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void DetectPlayerAndAttack()
    {
        Vector2 rayDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, rayDirection, detectionRange, playerLayers);

        // Vẽ đường raycast trong Scene view
        if (hitInfo.collider != null && hitInfo.collider.CompareTag("Player"))
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if (!playerInSight)
            {
                playerInSight = true; // Chỉ thay đổi khi player vào tầm nhìn
            }

            // Di chuyển về phía người chơi
            MoveTowardsPlayer();

            // Kiểm tra thời gian tấn công
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time; // Cập nhật thời gian tấn công
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + (Vector3)rayDirection * detectionRange, Color.yellow);
            playerInSight = false;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<Player>().TakeDamage(15);
        }
    }

    private void MoveTowardsPlayer()
    {
        if (Player == null)
        {
            animator.SetBool("isRunning", false);
            return;
        }
        // Lấy vị trí Player và di chuyển quái về phía đó
        Vector2 targetPosition = Player.transform.position;
        Vector2 currentPosition = transform.position;

        // Di chuyển quái về phía Player
        animator.SetBool("isRunning", true);
        Vector2 direction = (targetPosition - currentPosition).normalized; // Hướng di chuyển
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y); // Cập nhật vận tốc

        // Dừng di chuyển nếu đến gần
        float distance = Vector2.Distance(currentPosition, targetPosition);
        if (distance <= attackRange+0.3f)
        {
            animator.SetBool("isRunning", false);
            rb.linearVelocity = Vector2.zero; // Dừng lại khi đủ gần
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * detectionRange);
    }

    void Update()
    {
        Vector3 scale = transform.localScale;
        if (Player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        transform.localScale = scale;

        // Gọi hàm phát hiện và di chuyển
        DetectPlayerAndAttack();
    }
}
