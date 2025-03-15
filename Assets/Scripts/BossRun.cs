using Unity.VisualScripting;
using UnityEngine;

public class BossRun : MonoBehaviour
{
    public Animator animator;
    public GameObject Player;
    public GameObject spellPrefab;  // The prefab for the spell
    public Transform spellSpawnPoint;  // The point where the spell will spawn
    public bool flip;
    public float attackRange = 0.19f;
    public float detectionRange = 0.42f;
    public LayerMask playerLayers;
    public GameObject MushRum1;
    public GameObject MushRum2;
    public GameObject Bat;
    public GameObject Skeleton;
    public GameObject Skeleton2;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform attackPoint;
    private bool playerInSight = false;
    private bool isRunning = false;  // To track if boss is moving
    public float attackCooldown = 1.75f; // Thời gian chờ giữa các lần tấn công
    private float lastAttackTime = 0f; // Thời gian của lần tấn công cuối cùng
    private int attackCount = 0; // Counter for the number of attacks
    public float moveSpeed = 2f; // Speed at which the boss moves toward the player
    private bool summon = false;

    private void DetectPlayerAndAttack()
    {
        // Xác định hướng của boss đối với player (left hoặc right)
        Vector2 rayDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, rayDirection, detectionRange, playerLayers);

        if (hitInfo.collider != null && hitInfo.collider.CompareTag("Player"))
        {
            if (!playerInSight)
            {
                playerInSight = true; // Player đã bị phát hiện
                isRunning = true;  // Bắt đầu di chuyển về phía player
                MoveTowardsPlayer();
            }

            // Nếu boss trong phạm vi detectionRange nhưng ngoài attackRange, tiếp tục di chuyển về phía player
            if (isRunning && Vector2.Distance(transform.position, Player.transform.position) > attackRange)
            {
                MoveTowardsPlayer();
            }

            // Kiểm tra thời gian tấn công khi player ở trong attackRange
            if (Time.time >= lastAttackTime + attackCooldown && Vector2.Distance(transform.position, Player.transform.position) <= attackRange)
            {
                Attack();
                lastAttackTime = Time.time; // Cập nhật thời gian tấn công
            }
        }
        else
        {
            // Player ra khỏi phạm vi detectionRange, thi triển spell sau 1 giây
            while (playerInSight) // Nếu player đã bị phát hiện trước đó
            {
                // Gọi thi triển spell sau 1 giây
                Invoke("CastSpell", 0.5f);  // Trì hoãn việc thi triển spell 1 giây
                playerInSight = false; // Reset lại trạng thái phát hiện player
            }
            isRunning = false; // Dừng di chuyển khi player ra khỏi phạm vi detectionRange
        }
    }

    private void MoveTowardsPlayer()
    {
        animator.SetBool("isRunning", true);
        // Get the target position of the player but ignore the Y-axis
        Vector2 targetPosition = new Vector2(Player.transform.position.x, transform.position.y);

        // Move the boss towards the player on the X-axis
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        animator.SetBool("isRunning", false);
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<Player>().TakeDamage(10);
        }

        attackCount++; // Increase the attack counter

        // If the boss has attacked 5 times, spawn the spell
        if (attackCount >= 5)
        {
            CastSpell();
            attackCount = 0; // Reset the attack counter
        }

        isRunning = false;  // Stop running after attack
    }

    private void CastSpell()
    {

        // Set the "Cast" trigger for the animation
        animator.SetTrigger("Cast");

        // Triệu hồi phép thuật tại vị trí của người chơi
        GameObject spell = Instantiate(spellPrefab, Player.transform.position, Quaternion.identity);

        // Thiết lập trigger "Cast" cho hoạt ảnh
        animator.SetTrigger("Cast");

        // Xóa phép thuật sau 1 giây
        Destroy(spell, 0.5f);

        // Stop movement during spell cast
        isRunning = false;
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
        if(this.GetComponent<Enemy1>().currentHealth <= 200 && summon == false)
        {
            MushRum1.SetActive(true);
            MushRum2.SetActive(true);
            Bat.SetActive(true);
            Skeleton.SetActive(true);
            Skeleton2.SetActive(true);
            summon = true;
        }
        DetectPlayerAndAttack();
    }
}