using TMPro;
using UnityEngine;

internal class KeyboardViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text _upKey;
    [SerializeField] private TMP_Text _leftKey;
    [SerializeField] private TMP_Text _rightKey;

    [SerializeField] private Animation _upKeyAnimation;
    [SerializeField] private Animation _leftKeyAnimation;
    [SerializeField] private Animation _rightKeyAnimation;

    public void ChangeUpKeyColor(Color color)
    {
        _upKey.color = color;
    }

    public void ChangeLeftKeyColor(Color color)
    {
        _leftKey.color = color;
    }

    public void ChangeRightKeyColor(Color color)
    {
        _rightKey.color = color;
    }

    public void PlayUpKeyAnimation()
    {
        _upKeyAnimation.Play();
    }

    public void PlayLeftKeyAnimation()
    {
        _leftKeyAnimation.Play();
    }

    public void PlayRightKeyAnimation()
    {
        _rightKeyAnimation.Play();
    }

    public void ChangeTextUpKey(string text)
    {
        _upKey.text = text;
    }

    public void ChangeTextLeftKey(string text)
    {
        _leftKey.text = text;
    }

    public void ChangeTextRightKey(string text)
    {
        _rightKey.text = text;
    }
}
