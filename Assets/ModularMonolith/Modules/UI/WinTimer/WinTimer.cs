using TMPro;
using Zenject;
using UniRx;
using UnityEngine;

public class WinTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _totalTime = 60f;

    private MessageBroker _messageBroker;
    private CompositeDisposable _compositeDisposable;

    private float currentTime;
    private bool isRunning = false;

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
            .Subscribe(arg => StartTimer())
            .AddTo(_compositeDisposable);
        _messageBroker
            .Receive<PlayerLostEvent>()
            .Subscribe(arg => StopTimer())
            .AddTo(_compositeDisposable);
        _messageBroker
            .Receive<PlayerWinEvent>()
            .Subscribe(arg => StopTimer())
            .AddTo(_compositeDisposable);
    }

    private void OnDisable()
    {
        _compositeDisposable?.Dispose();
    }

    private void Start()
    {
        _timerText.enabled = false;
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimeText();

            if (currentTime <= 1f)
            {
                StopTimer();
                _messageBroker.Publish(new PlayerWinEvent());
            }
        }
    }

    private void StartTimer()
    {
        _timerText.enabled = true;
        currentTime = _totalTime;
        isRunning = true;
    }

    private void StopTimer()
    {
        _timerText.enabled = false;
        isRunning = false;
    }

    private void ResetTimer()
    {
        currentTime = _totalTime;
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
