﻿using System;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = nameof(GameData), menuName = "Static Data/" + nameof(GameData), order = 0)]
    public class GameData : ScriptableObject
    {
        [Header("Spawn")] [Tooltip("in seconds"), SerializeField]
        private float _spawnTimeSimplyEnemy = 1f;

        [Tooltip("in seconds"), SerializeField]
        private float _spawnTimeBossEnemy = 60f;

        [Tooltip("in percentages"), SerializeField, Range(0, 100)]
        private int _chanceSpawnInBottomAndTop = 65;

        [field: SerializeField, Header("Hero")]
        public int HeroDefaultHp { get; private set; } = 300;

        [field: SerializeField] public int MinimalDistanceForAttack { get; private set; } = 4;
        [field: SerializeField] public Vector2 HeroSpawnPoint { get; private set; } = Vector2.zero;
        [field: SerializeField] public float ArrowSpeed { get; private set; } = 18;
        [field: SerializeField] public float HeroDamage { get; private set; } = 5;
        [field: SerializeField] public float HeroCooldown { get; private set; } = .5f;

        [field: SerializeField, Header("Spells")]
        public SpellData Meteorite { get; private set; }

        public int SpawnTimeSimplyEnemy => (int)(_spawnTimeSimplyEnemy * 1000);
        public float SpawnTimeBossEnemy => _spawnTimeBossEnemy;
        public float ChanceSpawnInBottomAndTop => (float)_chanceSpawnInBottomAndTop / 100;
    }

    [Serializable]
    public class SpellData
    {
        [field: SerializeField] public float Cooldown { get; private set; } = 10;
        [field: SerializeField] public string Text { get; private set; } = "Текст";
    }
}