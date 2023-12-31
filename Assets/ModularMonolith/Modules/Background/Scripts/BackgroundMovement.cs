using UniRx;
using UnityEngine;
using Zenject;

internal class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

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
            .Subscribe(arg => enabled = false)
            .AddTo(_compositeDisposable);
        _messageBroker
            .Receive<PlayerLostEvent>()
            .Subscribe(arg => enabled = false)
            .AddTo(_compositeDisposable);
    }

    private void OnDisable()
    {
        _compositeDisposable?.Dispose();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * (Time.deltaTime * _speed));
    }
}
