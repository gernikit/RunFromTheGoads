using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Collider2D roomCollider;
    [SerializeField] private float _colliderSizeX;
    [SerializeField] private float _colliderSizeY;

    private void Start()
    {
        roomCollider = GetComponent<Collider2D>();
        Bounds bounds = roomCollider.bounds;
        bounds.size = new Vector2(_colliderSizeX, _colliderSizeY);
    }

    public float GetWitdh()
    {
        return _colliderSizeX;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector2(_colliderSizeX, _colliderSizeY));
    }
}
