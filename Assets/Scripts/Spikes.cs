using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damage = 100f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra tag của đối tượng va chạm
        {
            Player player = collision.GetComponent<Player>(); // Lấy script Player
            if (player != null)
            {
                player.TakeDamage(damage); // Gây sát thương cho người chơi
            }
        }
    }
}
