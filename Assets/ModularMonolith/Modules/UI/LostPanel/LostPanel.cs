using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LostPanel : MonoBehaviour
{
    [SerializeField] private GameObject _windowPanel;
    [SerializeField] private Button _lostButton;

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
            .Receive<PlayerLostEvent>()
            .Subscribe(_ => _windowPanel.SetActive(true))
            .AddTo(_compositeDisposable);
        _lostButton.onClick.AddListener(RestartLevel);
    }

    private void OnDisable()
    {
        _lostButton.onClick.RemoveListener(RestartLevel);
        _compositeDisposable.Dispose();
    }

    private void RestartLevel()
    {
        _messageBroker.Publish(new LevelRestartEvent());
        gameObject.SetActive(false);
    }
}
