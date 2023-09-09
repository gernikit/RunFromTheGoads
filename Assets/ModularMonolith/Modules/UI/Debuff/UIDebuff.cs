using UnityEngine;

internal class UIDebuff : MonoBehaviour
{
    [SerializeField] private Debuff _debuffType;

    public Debuff DebuffType => _debuffType;

    public void PlayOnAnimation()
    {
    }
}
