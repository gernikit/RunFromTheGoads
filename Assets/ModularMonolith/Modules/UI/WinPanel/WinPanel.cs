using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameObject _windowPanel;
    [SerializeField] private Button _winButton;

    private MessageBroker _messageBroker;
    private CompositeDisposable _compositeDisposable;

    [Inject]
    private void Construct(MessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    private void OnEnable()
    {
        _compositeDisposable = new CompositeDisposable();
        _messageBroker
            .Receive<PlayerWinEvent>()
            .Subscribe(arg => PlayerWin())
            .AddTo(_compositeDisposable);
        _winButton.onClick.AddListener(RestartLevel);
    }

    private void OnDisable()
    {
        _winButton.onClick.RemoveListener(RestartLevel);
        _compositeDisposable?.Dispose();
    }

    private void Start()
    {
        _windowPanel.SetActive(false);
    }

    private void PlayerWin()
    {
        _windowPanel.SetActive(true);
    }

    private void RestartLevel()
    {
        _messageBroker.Publish(new LevelRestartEvent());
    }
}
