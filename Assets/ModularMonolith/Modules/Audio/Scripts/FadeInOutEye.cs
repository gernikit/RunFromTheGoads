using UnityEngine;

public class FadeInOutEye : MonoBehaviour
{
    [SerializeField] private float _fadeInDuration = 1f;
    [SerializeField] private float _fadeOutDuration = 1f;
    [SerializeField] private float _idleDuration = 1f;

    private SpriteRenderer _spriteRenderer;
    private float _alpha = 0f;

    private float _minFadeIn = 0f;
    private float _maxFadeIn = 1f;
    private float _minFadeOut = 0f;
    private float _maxFadeOut = 1f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartFullFadeOut()
    {
        _minFadeOut = 0f;
        _maxFadeOut = _alpha;
        StartCoroutine(FadeOutRoutine());
    }

    public void StartFullFadeIn()
    {
        _minFadeIn = _alpha;
        _maxFadeIn = 1f;
        StartCoroutine(FadeInRoutine());
    }

    public void StartFadeIn(float minAlpha = 0f, float maxAlpha = 1f)
    {
        _maxFadeIn = maxAlpha;
        _minFadeIn = minAlpha;
        StartCoroutine(FadeInRoutine());
    }

    public void StartFadeOut(float minAlpha = 0f, float maxAlpha = 1f)
    {
        _maxFadeOut = maxAlpha;
        _minFadeIn = minAlpha;
        StartCoroutine(FadeOutRoutine());
    }

    private void SetSpriteAlpha(float value)
    {
        var color = _spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, value);
        _spriteRenderer.color = color;
    }

    private System.Collections.IEnumerator FadeInRoutine()
    {
        float timer = 0f;

        while (timer < _fadeInDuration)
        {
            timer += Time.deltaTime;
            _alpha = Mathf.Lerp(_minFadeIn, _maxFadeIn, timer / _fadeInDuration);
            SetSpriteAlpha(_alpha);
            yield return null;
        }

        Invoke(nameof(FadeOutRoutine), _idleDuration);
    }

    private System.Collections.IEnumerator FadeOutRoutine()
    {
        float timer = 0f;

        while (timer < _fadeOutDuration)
        {
            timer += Time.deltaTime;
            _alpha = Mathf.Lerp(_maxFadeOut, _minFadeOut, timer / _fadeOutDuration);
            SetSpriteAlpha(_alpha);
            yield return null;
        }
    }
}
