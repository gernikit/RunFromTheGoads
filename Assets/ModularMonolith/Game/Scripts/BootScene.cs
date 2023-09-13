using UnityEngine;
using UnityEngine.SceneManagement;

public class BootScene : MonoBehaviour
{
    [SerializeField] private string _startScene;
    void Start()
    {
        SceneManager.LoadScene(_startScene);
    }
}
