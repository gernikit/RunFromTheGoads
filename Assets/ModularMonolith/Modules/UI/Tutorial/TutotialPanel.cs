using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutotialPanel : MonoBehaviour
{
    [SerializeField] private Button _mainButton;
    [SerializeField] private List<GameObject> _advices;
    [SerializeField] private SceneAsset _gameLevel;

    private ushort _currentAdviceIndex = 0;

    private void OnEnable()
    {
        _mainButton.onClick.AddListener(SnowNextAdvice);
    }

    private void OnDisable()
    {
        _mainButton.onClick.RemoveListener(SnowNextAdvice);
    }

    private void SnowNextAdvice()
    {
        _advices[_currentAdviceIndex].SetActive(false);
        _currentAdviceIndex++;

        if (_advices.Count == _currentAdviceIndex)
        {
            SceneManager.LoadScene(_gameLevel.name);
            return;
        }

        _advices[_currentAdviceIndex].SetActive(true);

    }
}
