using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPlayerDied;
    [SerializeField] private Transform _player;
    [SerializeField] private SceneAsset _currentScene;

    private Camera _mainCamera;
    private Vector2 _screenBounds;

    private void Update()
    {
        if (IsOutOfBounds(_player.position))
        {
            OnPlayerDied.Invoke();
        }
    }

    private Vector2 CalculateScreenBounds()
    {
        Vector2 screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));
        return new Vector2(screenBounds.x, screenBounds.y);
    }

    private bool IsOutOfBounds(Vector3 position)
    {
        return (position.x < -_screenBounds.x || position.x > _screenBounds.x || position.y < -_screenBounds.y || position.y > _screenBounds.y);
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _screenBounds = CalculateScreenBounds();
        PauseGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_currentScene.name);
    }
}
