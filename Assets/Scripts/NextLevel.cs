using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private AudioSource completeSound;
    [SerializeField] private AudioSource wuhuSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        completeSound.Play();
        wuhuSound.Play();
        Invoke("CompleteLevel", 1);
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
