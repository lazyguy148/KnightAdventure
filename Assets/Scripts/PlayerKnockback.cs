using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    public float knockbackForce = 10f; // Lực đẩy ngang
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với Boss
        if (collision.collider.CompareTag("Boss"))
        {
            ApplyKnockback(collision);
        }
    }

    void ApplyKnockback(Collision2D collision)
    {
        // Tính hướng đẩy lùi (chỉ trên trục X)
        Vector2 knockbackDirection = new Vector2(
            (transform.position.x - collision.transform.position.x),
            0 // Đảm bảo không có lực trên trục Y
        ).normalized;

        // Áp dụng lực đẩy ngang cho player
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }
}
