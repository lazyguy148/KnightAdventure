using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform posA, posB;
    public int speed;
    Vector2 targetPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPos = posB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, posA.position) < .1f) targetPos = posB.position;
        if(Vector2.Distance(transform.position, posB.position) < .1f) targetPos = posA.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
