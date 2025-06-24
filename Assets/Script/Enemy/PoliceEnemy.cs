using UnityEngine;

public class PoliceEnemy : MonoBehaviour
{
    //variables
    [Header("Movement")]
    [SerializeField] bool canMove = true;
    [SerializeField] float speed = 10f;
    [SerializeField] Vector3 rightLimit;
    [SerializeField] Vector3 leftLimit;

    [Header("Check Ground")]
    [SerializeField] Vector2 groundCheckerBoxSize;
    [SerializeField] float groundCheckerCastDistance;
    [SerializeField] LayerMask groundLayer;
    int direction; // 1 para derecha, -1 para izquierda

    //properties

    //methods
    private void Update()
    {
        Move();
    }

    void Move()
    {
        if(transform.position.x == rightLimit.x)
        {
            direction = -1;
        }
        else if(transform.position == leftLimit)
        {
            direction = 1;
        }

        if(direction == -1)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftLimit, speed * Time.deltaTime);
        }
        else if(direction == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightLimit, speed * Time.deltaTime);
        }
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, groundCheckerBoxSize, 0, -transform.up, groundCheckerCastDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
