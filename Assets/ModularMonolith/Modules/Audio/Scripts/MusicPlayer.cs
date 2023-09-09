using UniRx;
using UnityEngine;
using Zenject;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _firstMusic;
    [SerializeField] private AudioClip _secondMusic;

    private MessageBroker _messageBroker;
    private CompositeDisposable _compositeDisposable;

    private AudioSource _currentAudioSource;

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
            .Subscribe(arg =>
            {
                SetLoop(false);
                PlaySecondMusic();
            })
            .AddTo(_compositeDisposable);
        _messageBroker
            .Receive<PlayerLostEvent>()
            .Subscribe(arg => StopPlayback())
            .AddTo(_compositeDisposable);
    }

    private void OnDisable()
    {
        _compositeDisposable?.Dispose();
    }

    private void Start()
    {
        _currentAudioSource = GetComponent<AudioSource>();
        _currentAudioSource.clip = _firstMusic;
        _currentAudioSource.loop = true;
        _currentAudioSource.Play();
    }

    private void StopPlayback()
    {
        _currentAudioSource.Stop();
    }

    public void SetLoop(bool loop)
    {
        _currentAudioSource.loop = loop;
    }

    public void PlaySecondMusic()
    {
        _currentAudioSource.clip = _secondMusic;
        _currentAudioSource.Play();
    }
}
