using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Animator animator;
    public GameObject healthPickupPrefab; // Tham chiếu đến prefab vật phẩm
    public float dropChance = 0.3f; // Xác suất rơi ra vật phẩm (30%)
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource swordSound;
    [SerializeField] private AudioSource deadSound;
    public void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        
    }

    [System.Obsolete]
    public virtual void TakeDamage(float damage)
    {
        swordSound.Play();
        hurtSound.Play();
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    public virtual void Die()
    {
        ScoreManager.instance.AddScore(50);
        // Kiểm tra tag của đối tượng
        if (gameObject.CompareTag("Skeleton"))
        {
            GetComponent<SKELETON1>().enabled = false;
        }
        else if (gameObject.CompareTag("MushRum"))
        {
            GetComponent<MushRum>().enabled = false;
        }
        else if (gameObject.CompareTag("DeadEye"))
        {
            GetComponent<EnemyShooting>().enabled = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            GetComponent<BossRun>().enabled = false;
        }
        animator.SetBool("Death", true);
        deadSound.Play();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        if(this.CompareTag("Boss"))
            Invoke("DropItem", 3f);
        else
            Invoke("DropItem", 0f);
        Destroy(gameObject, 3);
    }

    private void DropItem()
    {
        float randomValue = Random.Range(0f, 1f); // Sinh giá trị ngẫu nhiên từ 0 đến 1
        if (randomValue <= dropChance)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity); // Sinh vật phẩm tại vị trí enemy
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(20);
        }
    }
}
