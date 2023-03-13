using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = nameof(BloodlustData),
        menuName = "Static Data/Spells/" + nameof(BloodlustData))]
    public class BloodlustData : SpellData
    {
        [field: SerializeField, Tooltip("in percentages"), Range(0, 100)]
        public int Vampirism { get; private set; } = 5;

        [field: SerializeField, Tooltip("in percentages"), Range(0, 100)]
        public int AdditionalDamage { get; private set; } = 5;
    }
}