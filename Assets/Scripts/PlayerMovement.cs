using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _leftSpeedRatio = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField, Min(0)] private Vector2 _groundCollisionSize;
    [SerializeField, Min(0)] private float _groundCollisionDistance;
    [SerializeField, Min(0)] private float _groundCollisionAngle;

    private IInput _input;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private bool _grounded;
    private float _horizontal;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ReadInput();

        if (_input.Jump())
        {
            UpdateGroundCollisions();
            TryJump();
        }
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    public void Initialize(IInput input)
    {
        _input = input;
    }

    public void ChangeInput(IInput input)
    {
        _input = input;
    }

    private void Move()
    {
        Vector2 movement;
        if (_horizontal < 0)
            movement = new Vector2(_horizontal * _speed - _leftSpeedRatio, _rigidbody.velocity.y);
        else
            movement = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);

        _rigidbody.velocity = movement;
    }

    private void Rotate()
    {
        if (_rigidbody.velocity.x < -0.1f)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }

    private void ReadInput()
    {
        _horizontal = _input.HorizontalMove();
    }

    private void TryJump()
    {
        if (_grounded)
        {
            Vector2 jump = new Vector2(_rigidbody.velocity.x, _jumpForce);
            _rigidbody.velocity = jump;
            _grounded = false;
        }
    }

    private void UpdateGroundCollisions()
    {
        _grounded = Physics2D.BoxCast(transform.position, _groundCollisionSize / 2f, _groundCollisionAngle,  Vector2.down, _groundCollisionDistance, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.down * _groundCollisionDistance / 2f, _groundCollisionSize);
    }
}
