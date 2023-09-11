using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private SceneAsset _currentScene;

    private bool _playerDied = false;

    private MessageBroker _messageBroker;
    private CompositeDisposable _compositeDisposable;

    private Camera _mainCamera;
    private Vector2 _screenBounds;

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
            .Subscribe(arg => ContinueGame())
            .AddTo(_compositeDisposable);
        _messageBroker
            .Receive<PlayerWinEvent>()
            .Subscribe(arg => enabled = false)
            .AddTo(_compositeDisposable);
        _messageBroker
            .Receive<LevelRestartEvent>()
            .Subscribe(arg => RestartLevel())
            .AddTo(_compositeDisposable);
    }

    private void OnDisable()
    {
        _compositeDisposable?.Dispose();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _screenBounds = CalculateScreenBounds();
        PauseGame();
    }
    private void Update()
    {
        if (_playerDied == false && IsOutOfBounds(_player.position))
        {
            _playerDied = true;
            _messageBroker.Publish(new PlayerLostEvent());
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_currentScene.name);
    }

    private Vector2 CalculateScreenBounds()
    {
        Vector2 screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));
        return new Vector2(screenBounds.x, screenBounds.y);
    }

    private bool IsOutOfBounds(Vector3 position)
    {
        return (position.x < -_screenBounds.x || position.x > _screenBounds.x || position.y < -_screenBounds.y || position.y > _screenBounds.y);
    }
}
