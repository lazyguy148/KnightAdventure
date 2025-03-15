using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 20f; // Lượng máu hồi
    public Animator animator;
    [SerializeField] private AudioSource healSound;
    [SerializeField] private AudioSource playerSound;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("collected", true);
            healSound?.Play();
            playerSound?.Play();
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Heal(healthAmount);
                Destroy(gameObject,(float)0.5);
            }
        }
    }
}
