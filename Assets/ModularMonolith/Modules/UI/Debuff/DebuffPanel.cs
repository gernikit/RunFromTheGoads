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

    public void EnableDebuff(Debuff debuffType)
    {
        _debuffs.First(debuff => debuff.DebuffType == debuffType).gameObject.SetActive(true);
    }

    public void DisableDebuff(Debuff debuffType)
    {
        _debuffs.First(debuff => debuff.DebuffType == debuffType).gameObject.SetActive(false);
    }

    public void DisableAllDebuffs()
    {
        foreach (var debuff in _debuffs)
        {
            debuff.gameObject.SetActive(false);
        }
    }
}
