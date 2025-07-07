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
        if (canMove)
        {
            PatrolMove();
        }
    }

    void PatrolMove()
    {
        if (transform.position.x == rightLimit.x)
        {
            direction = -1;
        }
        else if (transform.position.x == leftLimit.x)
        {
            direction = 1;
        }

        if (direction == -1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(leftLimit.x, transform.position.y), speed * Time.deltaTime);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(rightLimit.x, transform.position.y), speed * Time.deltaTime);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Idamageable damageable))
        {
            damageable.GetDamage();
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
