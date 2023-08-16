using UnityEngine;

internal class Room : MonoBehaviour
{
    [SerializeField] private Collider2D _roomCollider;
    [SerializeField] private float _colliderSizeX;
    [SerializeField] private float _colliderSizeY;

    private void Start()
    {
        _roomCollider = GetComponent<Collider2D>();
        Bounds bounds = _roomCollider.bounds;
        bounds.size = new Vector2(_colliderSizeX, _colliderSizeY);
    }

    public float GetWidth()
    {
        return _colliderSizeX;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector2(_colliderSizeX, _colliderSizeY));
    }
}
