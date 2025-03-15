using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Object bullet;
    public Transform bulletPos;
    public Animator animator;
    public GameObject player;
    private float timer;
    public bool flip;
    [SerializeField] public AudioSource attackSound;

    // Các biến mới cho chuyển động parabol
    public float speed = 2f;      // Tốc độ di chuyển
    public float amplitude = 1f; // Biên độ (độ cao) của parabol
    private float time;           // Thời gian để tính vị trí trên parabol

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Điều chỉnh hướng quái
        AdjustDirection();

        // Di chuyển quái theo parabol
        if(this.gameObject.CompareTag("DeadEye"))
            MoveInParabola();

        // Kiểm tra và thực hiện tấn công
        AttackPlayer();
    }

    void AdjustDirection()
    {
        Vector3 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        transform.localScale = scale;
    }

    void MoveInParabola()
    {
        time += Time.deltaTime * speed;

        // Xác định vị trí mới theo phương trình parabol
        float x = Mathf.PingPong(time, 4) - 2; // Di chuyển qua lại giữa -2 và 2
        float y = amplitude * Mathf.Sin(x * Mathf.PI); // Tạo chuyển động parabol

        // Cập nhật vị trí quái
        transform.position = new Vector3(transform.position.x + x * Time.deltaTime, transform.position.y + y * Time.deltaTime, transform.position.z);
    }

    void AttackPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 2)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
                animator.SetTrigger("Attack");
                attackSound.Play();
            }
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
