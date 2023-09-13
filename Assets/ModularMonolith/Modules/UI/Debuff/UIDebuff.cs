using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

internal class UIDebuff : MonoBehaviour
{
    [SerializeField] private Debuff _debuffType;
    [SerializeField] private Image _image;
    [SerializeField] private float _animationDuration = 3f;
    [SerializeField] private float _scaleRatio = 2f;

    private Vector2 _startPosition;

    public Debuff DebuffType => _debuffType;

    private void OnEnable()
    {
        _image.enabled = false;
        _startPosition = transform.position;
    }

    public void PlayOnAnimation(Vector2 startAnimationPos)
    {
        transform.DOMove(startAnimationPos, 0);
        transform.DOScale(Vector2.one * _scaleRatio, _animationDuration)
            .OnComplete(() =>
                {
                    transform.DOMove(_startPosition, _animationDuration);
                    transform.DOScale(Vector2.one, _animationDuration);
                }
            );
        _image.enabled = true;
    }

    public async Task PlayOffAnimation()
    {
        transform.DOScale(0, _animationDuration);
        while (DOTween.IsTweening(transform))
        {
            await Task.Yield();
        }
    }
}
