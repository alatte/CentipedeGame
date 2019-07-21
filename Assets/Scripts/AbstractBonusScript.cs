using UnityEngine;


public abstract class AbstractBonusScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    protected PlayerScript playerScript;

    protected abstract void Action();

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Action();
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}



