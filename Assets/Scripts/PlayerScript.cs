using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject shot;
    public GameObject gun;
    public GameObject gameController;

    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private float shotDelay = 0.3f;
    [SerializeField]
    private int lifes = 3;
    private Vector2 direction;
    private float nextShot;
    private float height;
    private float width;
    private float shipRadius = 0.5f;

    private GameControllerScript gameControllerScript;

    private void Start()
    {
        gameControllerScript = gameController.GetComponent<GameControllerScript>();
        gameControllerScript.WriteLifes(lifes);

        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    void Update()
    {
        GetInput();
        Move();
        MovementRestriction();
    }

    //Moves the player
    void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    //Allows player to shoot with delay
    void Attack()
    {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;
            Instantiate(shot, gun.transform.position, Quaternion.identity);
        }
    }

    //Restrictions player moving with camera
    void MovementRestriction()
    {
        float xPosition = Mathf.Clamp(transform.position.x, -width / 2 + shipRadius, width / 2 - shipRadius);
        float yPosition = Mathf.Clamp(transform.position.y, -height / 2 + shipRadius, height / 2 - shipRadius);
        transform.position = new Vector2(xPosition, yPosition);
    }
    
    //Listens player input
    void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
        if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
        if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
        if (Input.GetKey(KeyCode.D)) direction += Vector2.right;
        if (Input.GetKey(KeyCode.Space)) Attack();
    }

    public void AddLife()
    {
        gameControllerScript.WriteLifes(++lifes);
    }

    public void AddShipSpeed()
    {
        if (speed < 15)
            speed += 0.2f;
    }

    public void AddShotSpeed()
    {
        if(shotDelay > 0.1)
            shotDelay -= 0.02f; 
    }

    public void LoseLife()
    {
        gameControllerScript.WriteLifes(--lifes);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Centipede" || collision.tag == "Ant")
            LoseLife();
    }
}
