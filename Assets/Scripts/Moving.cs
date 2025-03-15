using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour
{
    public Transform pointA; // Điểm A
    public Transform pointB; // Điểm B
    public float speed = 1f; // Tốc độ di chuyển
    public float pauseDuration = 1f; // Thời gian dừng lại

    private Vector3 target; // Mục tiêu hiện tại
    private bool isWaiting = false; // Trạng thái dừng lại

    void Start()
    {
        target = pointA.position; // Bắt đầu với mục tiêu là điểm A
    }

    void Update()
    {
        // Nếu lưỡi cưa đang dừng lại, không di chuyển
        if (isWaiting)
            return;

        // Di chuyển lưỡi cưa về phía mục tiêu hiện tại
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Khi đến gần mục tiêu
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Đổi mục tiêu
            target = target == pointA.position ? pointB.position : pointA.position;

            // Chạy Coroutine để dừng lại
            if (!isWaiting) // Prevent starting another coroutine if one is already running
            {
                StartCoroutine(WaitAndMove());
            }
        }
    }

    // Coroutine để dừng lại trong khoảng thời gian đã chỉ định
    IEnumerator WaitAndMove()
    {
        // Đánh dấu trạng thái dừng lại
        isWaiting = true;

        // Dừng lại trong thời gian đã chỉ định
        yield return new WaitForSeconds(pauseDuration);

        // Sau thời gian dừng lại, tiếp tục di chuyển
        isWaiting = false;
    }
}
