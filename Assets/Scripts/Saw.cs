using UnityEngine;

public class Saw : MonoBehaviour
{
    public float damage = 20f; // Lượng sát thương mỗi lần gây
    public float damageInterval = 0.01f; // Thời gian giữa mỗi lần gây sát thương
    public float rotationSpeed = 500f; // Tốc độ xoay (độ/giây)

    private bool isPlayerInZone = false; // Kiểm tra nếu người chơi đang trong vùng va chạm
    private float damageTimer = 0f; // Bộ đếm thời gian

    private void Update()
    {
        // Xoay quanh trục Z
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Nếu người chơi trong vùng va chạm, tăng timer
        if (isPlayerInZone)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                DealDamage(); // Gây sát thương
                damageTimer = 0f; // Reset bộ đếm
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInZone = true; // Bắt đầu theo dõi thời gian
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInZone = false; // Ngừng theo dõi thời gian
            damageTimer = 0f; // Reset bộ đếm thời gian khi người chơi thoát vùng
        }
    }

    private void DealDamage()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage); // Gây sát thương
            }
        }
    }
}
