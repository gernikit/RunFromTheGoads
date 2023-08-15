using UnityEngine;

public class RoomDestroyer : MonoBehaviour
{
    [SerializeField] private LayerMask _roomLayer;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_roomLayer == (_roomLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(collision.gameObject);
        }
    }
}
