using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pausePanel; // Kéo PausePanel từ Canvas vào đây
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // Tiếp tục nếu đang ở trạng thái pause
            }
            else
            {
                PauseGame(); // Dừng game và hiển thị pause menu
            }
        }
    }
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Dừng toàn bộ game
        if (pausePanel != null)
            pausePanel.SetActive(true); // Hiển thị PausePanel
    }

    // Tiếp tục game
    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Tiếp tục game
        if (pausePanel != null)
            pausePanel.SetActive(false); // Ẩn PausePanel
    }

    // Nút thoát game
    public void ExitGame()
    {
        Debug.Log("Tắt Pause Menu và tiếp tục game");
        isPaused = false;
        Time.timeScale = 1f; // Tiếp tục game
        pausePanel.SetActive(false); // Tắt Pause Panel
    }

    // Nút chơi lại màn chơi
    public void RestartGame()
    {
        Debug.Log("Chơi lại!");
        Time.timeScale = 1f; // Đảm bảo game tiếp tục khi chơi lại
        ScoreManager.instance.ResetScore(); // Reset điểm về 0
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        Debug.Log("Quay lại Main Menu");
        Time.timeScale = 1f; // Đảm bảo game tiếp tục khi quay lại menu chính
        ScoreManager.instance.ResetScore();
        SceneManager.LoadScene(0); // Tải lại Scene đầu tiên (Main Menu)
    }
}
