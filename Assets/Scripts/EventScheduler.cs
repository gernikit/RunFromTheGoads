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


    private void Start()
    {
        Invoke("ChangeBrokenHorizontalInput", 10);
        Invoke("ChangeBrokenJumpInput", 25);
        Invoke("ChangeBrokenAllInput", 40);
    }

    private void ChangeBrokenHorizontalInput()
    {
        _godEye.StartFadeIn(0, _maxAlpha);
        var brokenInput = new BrokenHorizontalInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();
        _leftKey.text = brokenInput.BrokenLeft;
        _leftKey.color = Color.red;
        _rightKey.text = brokenInput.BrokenRight;
        _rightKey.color = Color.red;
    }

    private void ChangeBrokenJumpInput()
    {
        _godEye.StartFadeIn(_maxAlpha, _maxAlpha * 2);
        var brokenInput = new BrokenJumpInput();
        _player.ChangeInput(brokenInput);
        _audioSource.Play();
        _upKey.text = brokenInput.BrokenJump;
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
        _upKey.text = brokenInput.BrokenJump;
        _leftKey.text = brokenInput.BrokenLeft;
        _rightKey.text = brokenInput.BrokenRight;
        _rightKey.color = Color.red;
        _leftKey.color = Color.red;
        _upKey.color = Color.red;

    }

}

public class BrokenHorizontalInput : IInput
{
    public string BrokenRight => "A";
    public string BrokenLeft => "D";

    private const string HorizontalAxis = "Horizontal";

    public float HorizontalMove()
    {
        return -Input.GetAxis(HorizontalAxis);
    }

    public bool Jump()
    {
        return Input.GetKeyDown(KeyCode.W);
    }
}

public class BrokenJumpInput : IInput
{
    private const string HorizontalAxis = "Horizontal";
    public string BrokenJump => "S";

    public float HorizontalMove()
    {
        return Input.GetAxis(HorizontalAxis);
    }

    public bool Jump()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
}

public class BrokenAllInput : IInput
{
    public string BrokenRight => "A";
    public string BrokenLeft => "D";
    public string BrokenJump => "S";

    private const string HorizontalAxis = "Horizontal";

    public float HorizontalMove()
    {
        return -Input.GetAxis(HorizontalAxis);
    }

    public bool Jump()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
}