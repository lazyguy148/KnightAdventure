using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject bossPrefab;  // Boss prefab cần sinh ra

    private bool bossSpawned = false;  // Kiểm tra xem boss đã sinh ra hay chưa

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng va chạm là người chơi
        if (other.CompareTag("Player") && !bossSpawned)
        {
            Invoke("SpawnTheBoss", 1);
        }
        
    }

    void SpawnTheBoss()
    {
        bossPrefab.SetActive(true);
        bossSpawned = true;  // Đảm bảo boss chỉ sinh ra một lần
    }
}
