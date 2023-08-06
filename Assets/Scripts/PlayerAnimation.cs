using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidBody;

    private const string ANIMATION_FALLING = "Falling";
    private const string ANIMATION_JUMPING = "Jumping";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool isFalling = CheckFalling();
        bool isJumping = CheckJump();

        _animator.SetBool(ANIMATION_FALLING, isFalling);
        _animator.SetBool(ANIMATION_JUMPING, isJumping);
    }

    private bool CheckFalling()
    {
        return _rigidBody.velocity.y < -0.2f;
    }

    private bool CheckJump()
    {
        return _rigidBody.velocity.y > 0.2f;
    }
}
