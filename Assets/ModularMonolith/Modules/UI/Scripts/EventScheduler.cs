using UniRx;
using UnityEngine;
using Zenject;

internal class EventScheduler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private FadeInOutEye _godEye;
    [SerializeField] private DebuffPanel _debuffPanel;
    [SerializeField] private float _maxAlpha = 0.3f;

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
        CancelInvoke();
        _compositeDisposable?.Dispose();
    }

    private void Start()
    {
        Invoke(nameof(ChangeBrokenHorizontalInput), 10);
        Invoke(nameof(ChangeBrokenJumpInput), 25);
        Invoke(nameof(ChangeBrokenAllInput), 38);
    }

    private void ChangeBrokenHorizontalInput()
    {
        _godEye.StartFadeIn( _maxAlpha);
        var brokenInput = new BrokenHorizontalInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();

        _debuffPanel.EnableDebuff(Debuff.HorizontalControlBroken);
    }

    private void ChangeBrokenJumpInput()
    {
        _godEye.StartFadeIn(_maxAlpha * 2);
        var brokenInput = new BrokenJumpInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();

        _debuffPanel.DisableAllDebuffs();
        _debuffPanel.EnableDebuff(Debuff.JumpBroken);
    }

    private void ChangeBrokenAllInput()
    {
        _godEye.StartFadeIn(_maxAlpha * 3.3f);
        var brokenInput = new BrokenAllInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();

        _debuffPanel.EnableDebuff(Debuff.HorizontalControlBroken);
        _debuffPanel.EnableDebuff(Debuff.JumpBroken);
    }
}


public class BrokenInput : IInput
{
    protected const string HorizontalAxis = "Horizontal";

    public virtual float HorizontalMove()
    {
        return Input.GetAxis(HorizontalAxis);
    }

    public virtual bool Jump()
    {
        return Input.GetKeyDown(KeyCode.W);
    }
}

public class BrokenHorizontalInput : BrokenInput
{
    public override float HorizontalMove()
    {
        return -Input.GetAxis(HorizontalAxis);
    }

    public override bool Jump()
    {
        return Input.GetKeyDown(KeyCode.W);
    }
}

public class BrokenJumpInput : BrokenInput
{
    public override float HorizontalMove()
    {
        return Input.GetAxis(HorizontalAxis);
    }

    public override bool Jump()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
}

public class BrokenAllInput : BrokenInput
{
    public override float HorizontalMove()
    {
        return -Input.GetAxis(HorizontalAxis);
    }

    public override bool Jump()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
}
