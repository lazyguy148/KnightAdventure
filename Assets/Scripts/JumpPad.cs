using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private float bounce = 10f;
    public Animator animator;

    [SerializeField] private AudioSource jumpSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jumpSound.Play();
            animator.SetTrigger("Bouce");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}
