using UnityEngine;

public class Enemy_Vertical : MonoBehaviour
{
    [SerializeField] private float movementDistance2;
    [SerializeField] private float speed2;
    [SerializeField] private float damage2;
    private bool movingUp;
    private float leftEdge2;
    private float rightEdge2;


    private void Awake()
    {
        leftEdge2 = transform.position.y - movementDistance2;
        rightEdge2 = transform.position.y + movementDistance2;
    }

    private void Update()
    {
        if (movingUp)
        {
            if (transform.position.y > leftEdge2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed2 * Time.deltaTime, transform.position.z);
            }
            else
                movingUp = false;
        }
        else
        {
            if (transform.position.y < rightEdge2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed2 * Time.deltaTime, transform.position.z);
            }
            else
                movingUp = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage2);
        }
    }
}
