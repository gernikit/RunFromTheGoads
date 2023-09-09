using UniRx;
using UnityEngine;
using Zenject;

internal class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Collider2D _generationArea;
    [SerializeField] private GameObject[] _levelPrefabs;
    [SerializeField] private RoomSpeedTimer _roomSpeedTimer;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LayerMask _roomLayer;

    private float _roomSpeedRatio = 0;

    private MessageBroker _messageBroker;
    private CompositeDisposable _compositeDisposable;

    private bool GeneratingAllowed
    {
        get => _generationArea.enabled;
        set => _generationArea.enabled = value;
    }

    [Inject]
    private void Construct(MessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }


    private void OnEnable()
    {
        _compositeDisposable = new CompositeDisposable();
        _messageBroker
            .Receive<GameStartEvent>()
            .Subscribe(_ => GeneratingAllowed = true)
            .AddTo(_compositeDisposable);
        _messageBroker
            .Receive<PlayerLostEvent>()
            .Subscribe(_ => GeneratingAllowed = false)
            .AddTo(_compositeDisposable);
        _messageBroker
            .Receive<PlayerWinEvent>()
            .Subscribe(_ => GeneratingAllowed = false)
            .AddTo(_compositeDisposable);
    }

    private void OnDisable()
    {
        _compositeDisposable?.Dispose();
    }

    private void Start()
    {
        GeneratingAllowed = false;
    }

    private void Update()
    {
        UpdateSpeedRatio();
    }

    private void UpdateSpeedRatio()
    {
       _roomSpeedRatio = _roomSpeedTimer.GetSpeedIncrease();
    }
    private void GenerateRandomRoom()
    {
        int randomIndex = Random.Range(0, _levelPrefabs.Length);
        GameObject roomPrefab = _levelPrefabs[randomIndex];
        Room room = roomPrefab.GetComponent<Room>();
        float roomWidth = room.GetWidth();
        var roomPosition = new Vector2(_spawnPoint.position.x + roomWidth / 2, _spawnPoint.position.y);

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
