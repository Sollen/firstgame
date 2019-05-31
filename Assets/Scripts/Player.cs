using UnityEngine;

public class Player : MonoBehaviour
{
   

    [Header("Speed")]
    public float WalkSpeed = 3;
    public float JumpForce = 10;

    private float _walkTime;
    private float _walkCooldown = 0.1f;

    private MoveState moveState = MoveState.Idle;
    private DirectionState directionState = DirectionState.Right;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator animatorController;

    private bool onGround;

    public void MoveRight()
    {
        if (onGround)
        {
            moveState = MoveState.Walk;
            if (directionState == DirectionState.Left)
            {
                _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                directionState = DirectionState.Right;
            }

            _walkTime = _walkCooldown;
            animatorController.Play("Walk");
        }
    }

    public void MoveLeft()
    {
        if (onGround)
        {
            moveState = MoveState.Walk;
            if (directionState == DirectionState.Right)
            {
                _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                directionState = DirectionState.Left;
            }

            _walkTime = _walkCooldown;
            animatorController.Play("Walk");
        }
    }

    public void Jump()
    {
        if (onGround)
        {
            _rigidbody.velocity = Vector3.up * JumpForce * Time.deltaTime;
            moveState = MoveState.Jump;
            onGround = false;
            animatorController.Play("Jump");
        }
    }

    private void Idle()
    {
        moveState = MoveState.Idle;
        animatorController.Play("Idle");
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        directionState = transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
    }
    
    private void Update()
    {
        if (moveState == MoveState.Jump)
        {
            if (_rigidbody.velocity == Vector2.zero)
                Idle();
        }
        else if (moveState == MoveState.Walk)
        {
            _rigidbody.velocity = (directionState == DirectionState.Right ? Vector2.right : -Vector2.right) *
                                  WalkSpeed * Time.deltaTime;
            _walkTime -= Time.deltaTime;
            if (_walkTime <= 0) Idle();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            onGround = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            onGround = false;
    }
}

enum DirectionState
{
    Left,
    Right
}
enum MoveState
{
    Idle,
    Walk,
    Jump
}
