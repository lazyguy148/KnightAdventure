using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        // Lấy kích thước của sprite
        Vector2 spriteSize = sr.sprite.bounds.size;

        // Lấy kích thước của camera
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // Tính toán scale
        Vector3 newScale = transform.localScale;
        newScale.x = screenWidth / spriteSize.x;
        newScale.y = screenHeight / spriteSize.y;
        transform.localScale = newScale;
    }
}
