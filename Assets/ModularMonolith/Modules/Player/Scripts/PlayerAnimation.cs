using UnityEngine;
using Zenject;

internal class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidBody;

    private readonly int _falling = Animator.StringToHash("Falling");
    private readonly int _jumping = Animator.StringToHash("Jumping");

    private IInput _input;

    private const float JumpingVelocityRatio = 12f;
    private const float FallingVelocityRatio = -JumpingVelocityRatio;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool isFalling = CheckFalling();
        bool isJumping = CheckJump();

        _animator.SetBool(_falling, isFalling);
        _animator.SetBool(_jumping, isJumping);
    }

    private bool CheckFalling()
    {
        return _rigidBody.velocity.y < FallingVelocityRatio;
    }

    private bool CheckJump()
    {
        return _input.Jump() && _rigidBody.velocity.y > JumpingVelocityRatio;
    }
}
