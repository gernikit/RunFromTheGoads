using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootScene : MonoBehaviour
{
    [SerializeField] private SceneAsset _startScene;
    void Start()
    {
        SceneManager.LoadScene(_startScene.name);
    }
}
