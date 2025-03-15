using UnityEngine;
using UnityEngine.SceneManagement;

public class END : MonoBehaviour
{
    [SerializeField] private AudioSource victorySound;
    [SerializeField] private AudioSource wuhooSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player")))
        {
            victorySound.Play();
            wuhooSound.Play();
            Invoke("ENDING", 1f);
        }
        
    }

    private void ENDING()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
