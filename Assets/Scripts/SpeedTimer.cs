using UnityEngine;

public class SpeedTimer : MonoBehaviour
{
    [SerializeField] private float _speedIncreaseAmount = 0.003f;
    [SerializeField] private float _increaseInterval = 1f;
    [SerializeField] private float _currentIncreaseAmount = 0;

    private float _timer;

    private void Start()
    {
        _timer = _increaseInterval;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            IncreaseSpeed();
            _timer = _increaseInterval;
        }
    }

    public float GetSpeedIncrease()
    {
        return _currentIncreaseAmount;
    }

    private void IncreaseSpeed()
    {
        _currentIncreaseAmount += _speedIncreaseAmount;
    }
}
