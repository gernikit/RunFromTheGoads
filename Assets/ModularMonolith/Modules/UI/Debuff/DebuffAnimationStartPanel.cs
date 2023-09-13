using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class DebuffAnimationStartPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _debuffPlace;
    [SerializeField] private RectTransform _rectTransform;

    private int _activedDebuffs = 0;

    private void Start()
    {
        _debuffPlace.ForEach(debuff => debuff.SetActive(false));
    }

    public Vector2 AddDebuffPlace()
    {
        if (_activedDebuffs + 1 > _debuffPlace.Count)
            throw new Exception("Count of activating debuffs too great");

        _debuffPlace[_activedDebuffs].SetActive(true);
        _activedDebuffs += 1;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);

        return _debuffPlace[_activedDebuffs - 1].transform.position;
    }

    public List<Vector2> AddDebuffPlaces(int count)
    {
        var newPlaces = new List<Vector2>();

        if (_activedDebuffs + count > _debuffPlace.Count)
            throw new Exception("Count of activating debuffs too great");

        for (int i = _activedDebuffs; i < _activedDebuffs + count; i++)
        {
            _debuffPlace[i].SetActive(true);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);

        for (int i = _activedDebuffs; i < _activedDebuffs + count; i++)
        {
            newPlaces.Add(_debuffPlace[i].transform.position);
        }

        _activedDebuffs += count;

        return newPlaces;
    }

    public void RemoveLastDebuffPlace()
    {
        if (_activedDebuffs - 1 < 0)
            throw new Exception("Count of deactivating debuffs too great");

        _activedDebuffs -= 1;
        _debuffPlace[_activedDebuffs].SetActive(false);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
    }

    public void RemoveAllPlaces()
    {
        for (int i = 0; i < _activedDebuffs; i++)
        {
            _debuffPlace[i].SetActive(false);
        }

        _activedDebuffs = 0;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
    }
}
