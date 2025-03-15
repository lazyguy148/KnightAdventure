using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Text storyText; // Text hiển thị cốt truyện (Legacy UI Text)
    public string[] storyLines; // Các dòng cốt truyện
    public float typingSpeed = 0.05f; // Tốc độ xuất hiện chữ
    public Text gameLogo; // Logo hoặc tên game (Legacy UI Text)
    public float logoDisplayTime = 3f; // Thời gian hiển thị logo
    public string nextSceneName; // Tên scene tiếp theo
    public GameObject button;
    private int currentLineIndex = 0;
    private bool isStoryFinished = false;

    void Start()
    {
        StartCoroutine(ShowIntro());
        Invoke("SkipAppear", 7f);
    }

    IEnumerator ShowIntro()
    {
        // Hiển thị cốt truyện
        storyText.gameObject.SetActive(true);
        while (currentLineIndex < storyLines.Length)
        {
            yield return StartCoroutine(TypeLine(storyLines[currentLineIndex]));
            currentLineIndex++;
            yield return new WaitForSeconds(1f); // Thời gian chờ giữa các dòng
        }

        isStoryFinished = true;

        // Sau khi hiển thị xong cốt truyện, ẩn cốt truyện đi
        storyText.gameObject.SetActive(false);

        // Hiển thị logo game (Đảm bảo logo được set active)
        gameLogo.gameObject.SetActive(true);

        // Hiển thị logo với hiệu ứng fade in và fade out
        yield return StartCoroutine(FadeIn(gameLogo, 2f)); // Logo xuất hiện trong 2 giây
        yield return new WaitForSeconds(logoDisplayTime); // Hiển thị logo trong 3 giây
        yield return StartCoroutine(FadeOut(gameLogo, 2f)); // Logo biến mất trong 2 giây


        // Chuyển sang scene tiếp theo
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator TypeLine(string line)
    {
        storyText.text = "";
        foreach (char c in line.ToCharArray())
        {
            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void SkipIntro()
    {
        if (!isStoryFinished)
        {
            StopAllCoroutines();
            SceneManager.LoadScene(nextSceneName);
        }
    }

    IEnumerator FadeText(Text text, float targetAlpha, float duration)
    {
        Color color = text.color;
        float startAlpha = color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            text.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        text.color = color;
    }
    IEnumerator FadeIn(Text text, float duration)
    {
        Color color = text.color;
        color.a = 0; // Đặt độ mờ ban đầu là 0
        text.color = color;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / duration); // Lerp từ 0 đến 1
            text.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1; // Đảm bảo độ mờ cuối cùng là 1
        text.color = color;
    }

    IEnumerator FadeOut(Text text, float duration)
    {
        Color color = text.color;
        color.a = 1; // Đặt độ mờ ban đầu là 1
        text.color = color;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / duration); // Lerp từ 1 xuống 0
            text.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0; // Đảm bảo độ mờ cuối cùng là 0
        text.color = color;
    }

    public void SkipAppear()
    {
        button.SetActive(true);
    }

}
