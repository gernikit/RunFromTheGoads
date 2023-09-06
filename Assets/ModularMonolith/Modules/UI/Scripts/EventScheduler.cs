using UnityEngine;

internal class EventScheduler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private FadeInOutEye _godEye;
    [SerializeField] private KeyboardViewController _keyboardViewController;
    [SerializeField] private float _maxAlpha = 0.3f;

    private void Start()
    {
        Invoke(nameof(ChangeBrokenHorizontalInput), 10);
        Invoke(nameof(ChangeBrokenJumpInput), 25);
        Invoke(nameof(ChangeBrokenAllInput), 38);
    }

    private void ChangeBrokenHorizontalInput()
    {
        _godEye.StartFadeIn(0, _maxAlpha);
        var brokenInput = new BrokenHorizontalInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();

        _keyboardViewController.PlayLeftKeyAnimation();
        _keyboardViewController.PlayRightKeyAnimation();

        _keyboardViewController.ChangeTextUpKey(brokenInput.JumpButton);
        _keyboardViewController.ChangeTextLeftKey(brokenInput.LeftButton);
        _keyboardViewController.ChangeTextRightKey(brokenInput.RightButton);

        _keyboardViewController.ChangeLeftKeyColor(Color.red);
        _keyboardViewController.ChangeRightKeyColor(Color.red);
    }

    private void ChangeBrokenJumpInput()
    {
        _godEye.StartFadeIn(_maxAlpha, _maxAlpha * 2);
        var brokenInput = new BrokenJumpInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();

        _keyboardViewController.ChangeTextUpKey(brokenInput.JumpButton);
        _keyboardViewController.ChangeTextLeftKey(brokenInput.LeftButton);
        _keyboardViewController.ChangeTextRightKey(brokenInput.RightButton);

        _keyboardViewController.PlayUpKeyAnimation();

        _keyboardViewController.ChangeUpKeyColor(Color.red);
        _keyboardViewController.ChangeLeftKeyColor(Color.blue);
        _keyboardViewController.ChangeRightKeyColor(Color.blue);
    }

    private void ChangeBrokenAllInput()
    {
        _godEye.StartFadeIn(_maxAlpha * 2, _maxAlpha * 3.3f);
        var brokenInput = new BrokenAllInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();

        _keyboardViewController.ChangeTextUpKey(brokenInput.JumpButton);
        _keyboardViewController.ChangeTextLeftKey(brokenInput.LeftButton);
        _keyboardViewController.ChangeTextRightKey(brokenInput.RightButton);

        _keyboardViewController.PlayUpKeyAnimation();
        _keyboardViewController.PlayLeftKeyAnimation();
        _keyboardViewController.PlayRightKeyAnimation();

        _keyboardViewController.ChangeUpKeyColor(Color.red);
        _keyboardViewController.ChangeLeftKeyColor(Color.red);
        _keyboardViewController.ChangeRightKeyColor(Color.red);
    }
}


public class BrokenInput : IInput
{
    public string RightButton { get; protected set; } = "D";
    public string LeftButton { get; protected set; } = "A";
    public string JumpButton { get; protected set; } = "W";

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

    public BrokenHorizontalInput()
    {
        RightButton = "A";
        LeftButton = "D";
    }

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
    public BrokenJumpInput()
    {
        JumpButton = "S";
    }

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
    public BrokenAllInput ()
    {
        RightButton = "A";
        LeftButton = "D";
        JumpButton = "S";
    }

    public override float HorizontalMove()
    {
        return -Input.GetAxis(HorizontalAxis);
    }

    public override bool Jump()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
}
