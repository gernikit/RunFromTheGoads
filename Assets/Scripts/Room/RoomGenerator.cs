using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelPrefabs;
    [SerializeField] private SpeedTimer _speedTimer;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LayerMask _roomLayer;

    private float _roomSpeedRatio = 0;

    private void Update()
    {
        UpdateSpeedRatio();
    }

    private void UpdateSpeedRatio()
    {
       _roomSpeedRatio = _speedTimer.GetSpeedIncrease();
    }
    private void GenerateRandomRoom()
    {
        int randomIndex = Random.Range(0, _levelPrefabs.Length);
        GameObject roomPrefab = _levelPrefabs[randomIndex];
        Room room = roomPrefab.GetComponent<Room>();
        float roomWidth = room.GetWitdh();
        Vector2 roomPosition = new Vector2(_spawnPoint.position.x + roomWidth / 2, _spawnPoint.position.y);

        GameObject newRoom = Instantiate(roomPrefab, roomPosition, Quaternion.identity);
        RoomMovement roomMovement = newRoom.GetComponent<RoomMovement>();
        roomMovement.SetSpeed(roomMovement.Speed + _roomSpeedRatio);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_roomLayer == (_roomLayer | (1 << collision.gameObject.layer)))
        {
            GenerateRandomRoom();
        }
    }
}
