using TMPro;
using UnityEngine;

public class EventScheduler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private FadeInOutEye _godEye;
    [SerializeField] private float _maxAlpha = 0.3f;

    [SerializeField] private TMP_Text _upKey;
    [SerializeField] private TMP_Text _leftKey;
    [SerializeField] private TMP_Text _rightKey;

    [SerializeField] private Animation _upKeyAnimation;
    [SerializeField] private Animation _leftKeyAnimation;
    [SerializeField] private Animation _rightKeyAnimation;


    private void Start()
    {
        Invoke("ChangeBrokenHorizontalInput", 10);
        Invoke("ChangeBrokenJumpInput", 25);
        Invoke("ChangeBrokenAllInput", 38);
    }

    private void ChangeBrokenHorizontalInput()
    {
        _godEye.StartFadeIn(0, _maxAlpha);
        var brokenInput = new BrokenHorizontalInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();
        SlowTimeFor(2);


        _leftKeyAnimation.Play();
        _rightKeyAnimation.Play();

        _upKey.text = brokenInput.JumpButton;
        _leftKey.text = brokenInput.LeftButton;
        _rightKey.text = brokenInput.RightButton;

        _leftKey.color = Color.red;
        _rightKey.color = Color.red;
    }

    private void ChangeBrokenJumpInput()
    {
        SlowTimeFor(3);
        _godEye.StartFadeIn(_maxAlpha, _maxAlpha * 2);
        var brokenInput = new BrokenJumpInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();
        SlowTimeFor(2);

        _upKey.text = brokenInput.JumpButton;
        _leftKey.text = brokenInput.LeftButton;
        _rightKey.text = brokenInput.RightButton;

        _upKeyAnimation.Play();

        _upKey.color = Color.red;
        _leftKey.color = Color.blue;
        _rightKey.color = Color.blue;
    }

    private void ChangeBrokenAllInput()
    {
        _godEye.StartFadeIn(_maxAlpha * 2, _maxAlpha * 3.3f);
        var brokenInput = new BrokenAllInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();
        SlowTimeFor(2);

        _upKey.text = brokenInput.JumpButton;
        _leftKey.text = brokenInput.LeftButton;
        _rightKey.text = brokenInput.RightButton;

        _leftKeyAnimation.Play();
        _rightKeyAnimation.Play();
        _upKeyAnimation.Play();

        _rightKey.color = Color.red;
        _leftKey.color = Color.red;
        _upKey.color = Color.red;
    }

    private void SlowTimeFor(int seconds)
    {
        Time.timeScale = 0.8f;
        Invoke("RestoreTimeScale", seconds);
    }

    private void RestoreTimeScale()
    {
        Time.timeScale = 1f;
    }
}

public class BrokenInput : IInput
{
    public string RightButton { get; set; } = "D";
    public string LeftButton { get; set; } = "A";
    public string JumpButton { get; set; } = "W";

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