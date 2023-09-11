using UnityEngine;
using DG.Tweening;
using UniRx;
using Zenject;

public class FadeInOutEye : MonoBehaviour
{
    [SerializeField] private float _fadeInDuration = 3f;
    [SerializeField] private float _fadeOutDuration = 3f;

    private SpriteRenderer _spriteRenderer;

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
            .Subscribe(_ => StartFullFadeOut())
            .AddTo(_compositeDisposable);
        _messageBroker
            .Receive<PlayerLostEvent>()
            .Subscribe(_ => StartFullFadeIn())
            .AddTo(_compositeDisposable);
    }

    private void OnDisable()
    {
        _compositeDisposable?.Dispose();
        _spriteRenderer.DOKill();
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartFullFadeOut()
    {
        _spriteRenderer.DOFade(0, _fadeOutDuration);
    }

    public void StartFullFadeIn()
    {
        _spriteRenderer.DOFade(1, _fadeInDuration);
    }

    public void StartFadeIn(float alpha)
    {
        _spriteRenderer.DOFade(alpha, _fadeInDuration);
    }
}
