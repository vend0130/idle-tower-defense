using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = nameof(GameData), menuName = "Static Data/" + nameof(GameData), order = 0)]
    public class GameData : ScriptableObject
    {
        [Header("Spawn")] [Tooltip("in seconds")] [SerializeField]
        private float _spawnTimeSimplyEnemy = 1f;

        [Tooltip("in seconds")] [SerializeField]
        private float _spawnTimeBossEnemy = 60f;

        [Tooltip("in percentages")] [SerializeField, Range(0, 100)]
        private int _chanceSpawnInBottomAndTop = 65;

        [field: SerializeField, Header("Hero")]
        public int HeroDefaultHp { get; private set; } = 300;

        [field: SerializeField] public int MinimalDistanceForAttack { get; private set; } = 4;
        [field: SerializeField] public Vector2 HeroSpawnPoint { get; private set; } = Vector2.zero;

        public int SpawnTimeSimplyEnemy => (int)(_spawnTimeSimplyEnemy * 1000);
        public float SpawnTimeBossEnemy => _spawnTimeBossEnemy;
        public float ChanceSpawnInBottomAndTop => (float)_chanceSpawnInBottomAndTop / 100;
    }
}