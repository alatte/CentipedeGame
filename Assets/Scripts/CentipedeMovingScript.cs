using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Well, I'll try to explain...
public class CentipedeMovingScript : MonoBehaviour
{
    public Sprite headSprite;
    public Sprite bodySprite;
    public GameObject mushroom;

    private float height;
    private float width;
    //For head moving
    private float speed;
    private GameObject target;
    private bool isHead = false;
    private bool leftDirection = false;
    private bool downDirection = false;
    private bool seeObstacle = false;
    private float headCentipedeRadius = 0.5f;
    private bool canMove = true;
    private bool moving = false;
    private Vector2 position;
    //For body moving
    private Vector2 previousPosition;
    private Quaternion previousRotation;
    private float distanceBetweenParts = 1f;

    void Start()
    {
        //Find head and set sprites for parts
        if (target == null)
        {
            isHead = true;
            GetComponent<SpriteRenderer>().sprite = headSprite;
        }
        else
        {
            isHead = false;
            GetComponent<SpriteRenderer>().sprite = bodySprite;
        }

        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    void Update()
    {
        if (isHead)
        {
            //If centipede isn't moving we'll set it new position to move
            if (canMove)
            {
                position = transform.position;
                MoveHead();
            }
            //If centipede is moving...
            if (moving)
            {
                //...we are waiting for it to reach new position
                if ((Vector2)transform.position == position)
                {
                    moving = false;
                    canMove = true;
                    MoveHead();
                }
            }
        }

        else MoveBody();
        //When all elements got new positions, lets 
        transform.position = Vector2.MoveTowards(transform.position, position, Time.deltaTime * speed);
        SaveCurrentPosition();
    }

    //Saves position for follower
    void SaveCurrentPosition()
    {
        previousPosition = new Vector2(transform.position.x, transform.position.y);
        previousRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    void MoveHead()
    {
        if (downDirection) MoveDown();
        else MoveRightOrLeft();
    }

    void MoveRightOrLeft()
    {
        if (leftDirection)
        {
            position += Vector2.left;
            transform.rotation = Quaternion.AngleAxis(GetAngleRotation(Vector2.left), Vector3.forward);
        }
        else
        {
            position += Vector2.right;
            transform.rotation = Quaternion.AngleAxis(GetAngleRotation(Vector2.right), Vector3.forward);
        }

        seeObstacle = false;
        canMove = false;
        moving = true;
    }

    void MoveDown()
    {
        position += Vector2.down;
        leftDirection = !leftDirection;
        transform.rotation = Quaternion.AngleAxis(GetAngleRotation(Vector2.down.normalized), Vector3.forward);

        downDirection = false;
        canMove = false;
        moving = true;
    }

    void MoveBody()
    {
        //If player destroys body element (target), we'll get new centipede
        if (target == null)
        {
            isHead = true;
            GetComponent<SpriteRenderer>().sprite = headSprite;
            AvoidObstacle();
        }
        else
        {
            MoveBodyOnNextPosition();
        }
    }

    //Get next position for body element
    void MoveBodyOnNextPosition()
    {
        float distance = Vector2.Distance(target.transform.position, transform.position);
        if (distance > distanceBetweenParts)
        {
            position = target.GetComponent<CentipedeMovingScript>().previousPosition;
            transform.rotation = target.GetComponent<CentipedeMovingScript>().previousRotation;
        }
    }

    //Get angle rotation by element's direction
    float GetAngleRotation(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }

    void AvoidObstacle()
    {
        canMove = true;
        seeObstacle = true;
        downDirection = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mushroom" && isHead && !seeObstacle) AvoidObstacle();
        if (other.tag == "Shot")
        {
            Instantiate(mushroom, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.tag == "SideWall" && isHead && !seeObstacle) AvoidObstacle();
        if (other.tag == "BottomWall" && isHead)
            transform.parent.GetComponent<CentipedeScript>().CollisionWithWallDetected(this);
    }

    public void SetCentipedeSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
