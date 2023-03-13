using UnityEngine;

namespace Code.Data
{
    public abstract class SpellData : ScriptableObject
    {
        [field: SerializeField] public string Text { get; private set; } = "Текст";
    }
}