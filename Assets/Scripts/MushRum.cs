using UnityEngine;
using System.Collections;

public class MushRum : MonoBehaviour
{
    public Transform pointA; // Điểm A
    public Transform pointB; // Điểm B
    public float speed = 1f; // Tốc độ di chuyển
    public Animator animator;

    private Vector3 target; // Mục tiêu hiện tại
    private bool facingRight = true; // Quái vật đang đối diện phải
    private bool isMoving = false; // Trạng thái di chuyển của quái vật
    private bool isWaiting = false; // Kiểm tra trạng thái chờ

    void Start()
    {
        target = pointA.position; // Bắt đầu với mục tiêu là điểm A
    }

    void Update()
    {
        // Nếu quái vật đang đứng yên (chờ), không di chuyển
        if (isWaiting)
            return;

        // Kiểm tra xem quái vật có đang di chuyển không
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            isMoving = true;
            animator.SetBool("IsRunning", true); // Bắt đầu hoạt ảnh chạy khi di chuyển
        }
        else
        {
            isMoving = false;
            animator.SetBool("IsRunning", false); // Dừng hoạt ảnh chạy khi không di chuyển
        }

        // Di chuyển quái vật về phía mục tiêu hiện tại
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Khi đến gần mục tiêu
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Đổi mục tiêu
            target = target == pointA.position ? pointB.position : pointA.position;

            // Lật mặt quái vật
            Flip();

            // Chạy Coroutine dừng lại 1 giây
            StartCoroutine(WaitAndMove());
        }
    }

    void Flip()
    {
        // Đổi trạng thái hướng mặt
        facingRight = !facingRight;

        // Nếu sprite mặc định hướng về trái, đổi dấu logic
        Vector3 localScale = transform.localScale;
        localScale.x = facingRight ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    // Coroutine để dừng lại 1 giây
    IEnumerator WaitAndMove()
    {
        // Đánh dấu trạng thái chờ
        isWaiting = true;

        animator.SetBool("IsRunning",false);

        // Dừng lại 1 giây
        yield return new WaitForSeconds(1f);

        // Sau 1 giây, tiếp tục di chuyển
        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra tag của đối tượng va chạm
        {
            Player player = collision.GetComponent<Player>(); // Lấy script Player
            if (player != null)
            {
                player.TakeDamage(20); // Gây sát thương cho người chơi
            }
        }
    }
}
