using UnityEngine;

public class Character : MonoBehaviour
{
    
    public float maxHealth = 100f;
    public float currentHealth;
    public Animator animator;

    public float attackRange = 0.22f;
    [SerializeField] private Transform attackPoint;
    public LayerMask playerLayers;


    public void Start()
    {
        currentHealth = maxHealth;
    }


    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0 )
        {
            Die();
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayers);
        if (hitPlayer != null)
        {
            hitPlayer.GetComponent<Character>().TakeDamage(30);
        }
    }

    public virtual void Die()
    {
        animator.SetBool("Death", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    
}
