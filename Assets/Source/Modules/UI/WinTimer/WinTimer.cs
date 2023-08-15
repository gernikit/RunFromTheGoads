using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WinTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _totalTime = 60f;

    public UnityEvent _timeIsOut;

    private float currentTime;
    private bool isRunning = false;

    private void Start()
    {
        StartTimer();
    }
    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimeText();

            if (currentTime <= 1f)
            {
                StopTimer();
                _timeIsOut.Invoke();
            }
        }
    }

    private void StartTimer()
    {
        currentTime = _totalTime;
        isRunning = true;
    }

    private void StopTimer()
    {
        isRunning = false;
    }

    private void ResetTimer()
    {
        currentTime = _totalTime;
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
