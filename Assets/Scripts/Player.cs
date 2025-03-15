using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Animator animator;
    public float knockbackForce = 50f;
    public float invincibilityDuration = 1.5f; // Thời gian bất tử sau khi nhận sát thương
    public Image healthBar;
    public GameObject gameOverPanel;
    

    private bool isInvincible = false; // Kiểm tra trạng thái bất tử
    private float invincibilityTimer = 0f; // Bộ đếm thời gian bất tử
    private Rigidbody2D rb;

    [SerializeField] private AudioSource deadSound;
    [SerializeField] private AudioSource hurtSound;


    private void Update()
    {
        // Đảm bảo phép chia float
        healthBar.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0, 1);

        // Kiểm tra giá trị currentHealth
        if (currentHealth < 0.01f)
        {
            currentHealth = 0;
        }
        // Kiểm tra nếu người chơi đang trong trạng thái bất tử
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }

        
    }

    


    public void Start()
    {
        currentHealth = maxHealth;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }


    public virtual void TakeDamage(float damage)
    {

        // Nếu đang trong trạng thái bất tử thì không nhận sát thương
        if (isInvincible) return;
        hurtSound.Play();
        ScoreManager.instance.SubtractScore(10);
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        // Kích hoạt trạng thái bất tử
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;


        if (currentHealth <= 0)
        {
            Invoke(nameof(Die), 0.2f);
        }

    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); // Hồi máu, không vượt quá maxHealth
        Debug.Log("Healed: " + amount + " HP. Current Health: " + currentHealth);
    }

    public void StopAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f; // Dừng thời gian trong game
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Hiển thị Game Over Panel
        }
    }


    public virtual void Die()
    {
        deadSound.Play();
        animator.SetBool("die", true);
        GetComponent<CharacterController>().enabled = false;
        GetComponent<Player>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Invoke("StopAnimator", 1.75f);
        Invoke("GameOver", 1.75f);
    }
}
