using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = nameof(MeteoriteData), menuName = "Static Data/Spells/" + nameof(MeteoriteData))]
    public class MeteoriteData : SpellData
    {
        [field: SerializeField] public float Cooldown { get; private set; } = 10;
        [field: SerializeField] public float Speed { get; private set; } = 20;
    }
}