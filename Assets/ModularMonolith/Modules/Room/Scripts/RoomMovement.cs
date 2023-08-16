using UnityEngine;

internal class RoomMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    public float Speed => _speed;

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * (_speed * Time.fixedDeltaTime));
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
