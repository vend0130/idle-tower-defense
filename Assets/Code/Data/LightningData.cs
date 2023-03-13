using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = nameof(LightningData), menuName = "Static Data/Spells/" + nameof(LightningData))]
    public class LightningData : SpellData
    {
        [field: SerializeField] public float Cooldown { get; private set; } = 5;
        [field: SerializeField] public float Radius { get; private set; } = 2;
        [field: SerializeField] public float Damage { get; private set; } = 7;
        [field: SerializeField] public int CountSteps { get; private set; } = 6;

        public int DelayToHide => 200;
    }
}