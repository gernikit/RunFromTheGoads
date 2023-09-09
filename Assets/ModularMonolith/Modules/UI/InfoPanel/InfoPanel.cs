using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private MessageBroker _messageBroker;

    [Inject]
    private void Construct(MessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        _messageBroker.Publish(new GameStartEvent());
        gameObject.SetActive(false);
    }
}
