using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal enum Debuff
{
    HorizontalControlBroken,
    JumpBroken
}
internal class DebuffPanel : MonoBehaviour
{
    [SerializeField] private UIDebuff [] _debuffs;
    [SerializeField] private DebuffAnimationStartPanel _debuffAnimationPanel;

    public void EnableDebuff(Debuff debuffType)
    {
        UIDebuff debuff = _debuffs.First(debuff => debuff.DebuffType == debuffType);
        debuff.gameObject.SetActive(true);
        debuff.PlayOnAnimation(_debuffAnimationPanel.AddDebuffPlace());
    }

    public void EnableTogetherFewDebuffs(List<Debuff> debuffs)
    {
        UIDebuff [] uiDebuffs = _debuffs.Where(deb => debuffs.Contains(deb.DebuffType)).ToArray();

        foreach (var debuff in uiDebuffs)
        {
            debuff.gameObject.SetActive(true);
        }

        List<Vector2> places = _debuffAnimationPanel.AddDebuffPlaces(debuffs.Count);
        int index = 0;

        foreach (var debuff in uiDebuffs)
        {
            debuff.PlayOnAnimation(places[index]);
            index++;
        }
    }

    public async void DisableDebuff(Debuff debuffType)
    {
        await _debuffs.First(debuff => debuff.DebuffType == debuffType).PlayOffAnimation();
        _debuffs.First(debuff => debuff.DebuffType == debuffType).gameObject.SetActive(false);
        _debuffAnimationPanel.RemoveLastDebuffPlace();
    }

    public void DisableAllDebuffs()
    {
        _debuffAnimationPanel.RemoveAllPlaces();

        Debuff[] debuffs = (Debuff[])Enum.GetValues(typeof(Debuff));

        foreach (var debuff  in debuffs)
        {
            DisableDebuff(debuff);
        }
    }
}
