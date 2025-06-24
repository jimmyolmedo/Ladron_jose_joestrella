using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class RigidbodyMovement : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] float speed = 200;
    [SerializeField] float maxGroundSpeed = 25f;
    [SerializeField] float maxAirSpeed = 10f;
    [SerializeField] float airControlSpeed = 100;
    [SerializeField] float deceleration = 10f;
    [SerializeField] float airDeceleration = 5f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float airAcceleration = 5f;
    public float currentVelocity = 0f;
    [SerializeField] float moveInput = 0;
    [SerializeField] int lastDirection = 0;
    [SerializeField] bool canMove = true;
    [SerializeField] bool isMoving;
    Vector2 move;
    public Vector2 referenceMove => move;
    [SerializeField]Rigidbody2D rb;

    [Header("Jump")]
    [SerializeField] float coyoteTime;
    [SerializeField] float jumpForce = 10;
    bool isJumping;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 groundCheckerBoxSize;
    [SerializeField] float groundCheckerCastDistance;

    //[Header("correccion de esquinas")]
    //[SerializeField] Transform ray1;
    //[SerializeField] Transform ray2;
    //[SerializeField] float rayDistance;
    //[SerializeField] LayerMask cornerLayer;

    private void Update()
    {
        if (isGrounded())
        {

        }
        else
        {
            coyoteTime += Time.deltaTime;
        }

        ////raycast esquinas

        //RaycastHit2D leftRay = Physics2D.Raycast(ray1.position, Vector2.up, rayDistance, cornerLayer);

        //RaycastHit2D rightRay = Physics2D.Raycast(ray2.position, Vector2.up, rayDistance, cornerLayer);

        //if(leftRay && !rightRay)
        //{
        //    transform.position += new Vector3(0.2f, 0);
        //}
        //else if(rightRay && !leftRay)
        //{
        //    transform.position -= new Vector3(0.2f, 0);
        //}
    }

    void FixedUpdate()
    {

        float maxSpeed = isGrounded() ? speed : airControlSpeed;
        if (canMove)
        {
            if (isGrounded())
            {
                HandleGroundMovement();
                isJumping = false;
                flip();
            }
            else
            {
                HandleAirMovement();
            }
        }

        rb.linearVelocity = new Vector2(currentVelocity, rb.linearVelocity.y);

    }

    private void HandleGroundMovement()
    {
        if (moveInput != 0)
        {
            // Movimiento con aceleraci�n en el suelo
            currentVelocity = Mathf.MoveTowards(currentVelocity, moveInput * speed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Detener gradualmente cuando no hay entrada
            currentVelocity = Mathf.MoveTowards(currentVelocity, 0, acceleration * Time.fixedDeltaTime);
        }

        currentVelocity = Mathf.Clamp(currentVelocity, -maxGroundSpeed, maxGroundSpeed);
    }

    private void HandleAirMovement()
    {
        if (moveInput != 0)
        {
            // Acelerar hacia la direcci�n de entrada
            currentVelocity = Mathf.MoveTowards(currentVelocity, moveInput * speed, airAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Mantener velocidad predeterminada en el aire si no hay entrada
            currentVelocity = Mathf.MoveTowards(currentVelocity, lastDirection * airControlSpeed, airDeceleration * Time.fixedDeltaTime);
        }

        // Desacelerar lentamente si intenta cambiar de direcci�n
        if (Mathf.Sign(moveInput) != Mathf.Sign(currentVelocity) && moveInput != 0)
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, 0, airDeceleration * Time.fixedDeltaTime);
        }

        // Limitar la velocidad horizontal en el aire
        currentVelocity = Mathf.Clamp(currentVelocity, -maxAirSpeed, maxAirSpeed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 moveDirection = context.ReadValue<Vector2>();
        moveInput = moveDirection.x;
        if (isGrounded())
        {
            move = new Vector2(moveInput, rb.linearVelocity.y);
        }
    }

    void flip()
    {
        if(move.x != 0)
        {
            Vector3 localscale = transform.localScale;
            localscale.x = Mathf.Sign(currentVelocity);
            
            transform.localScale = localscale;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (isJumping) return;
            if (isGrounded())
            {
                rb.AddForce(Vector2.up * jumpForce);
                isJumping = true;
            }
            else
            {
                if (coyoteTime <= 0.1f)
                {
                    rb.AddForce(Vector2.up * jumpForce);
                    isJumping = true;
                }
            }
        }
    }

   public void RecoverMovement()
    {
        canMove = true;
        if (isGrounded())
        {
            //Move(Vector2.zero);
        }
    }

    public void disableMovement()
    {
        currentVelocity = 0;
        canMove = false;
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


    public void InvokeInpulse(Vector3 _direction, float _force, float _impulseDuration)
    {
        canMove = false;
        rb.linearVelocity = new Vector2(_direction.x * _force, _direction.y * _force);

        Invoke(nameof(RecoverMovement), _impulseDuration);

        //rb.AddForce(_direction * _force);
    }

    public void PlatformMov(Vector2 move)
    {
        if(currentVelocity == 0)
        {
            transform.Translate(move);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckerCastDistance, groundCheckerBoxSize);
        Gizmos.color = Color.yellow;
        //Gizmos.DrawRay(ray1.position, Vector2.up * rayDistance);
        //Gizmos.DrawRay(ray2.position, Vector2.up * rayDistance);
    }

}
