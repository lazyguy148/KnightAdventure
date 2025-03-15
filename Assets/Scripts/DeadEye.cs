using UnityEngine;

public class DeadEye : MonoBehaviour
{
    public float maxHealth = 30f;
    public float currentHealth;
    public Animator animator;
    public GameObject Player;
    public bool flip;

    public void Start()
    {
        currentHealth = maxHealth;
    }
    [System.Obsolete]
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
            Die();
    }
    public virtual void Die()
    {
        animator.SetBool("Death", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    private void Update()
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
    }
}
