using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource quitSound;
    // Hàm xử lý sự kiện cho nút New Game
    public void NewGame()
    {
        startSound.Play();
        Invoke("LoadSceneOne", 1);
    }

    // Hàm xử lý sự kiện cho nút Quit
    public void QuitGame()
    {
        quitSound.Play();
        Debug.Log("Game Quitting...");
        Application.Quit(); // Đóng ứng dụng khi nhấn Quit
    }

    public void LoadSceneOne()
    {
        SceneManager.LoadScene("ONE");
    }
}
