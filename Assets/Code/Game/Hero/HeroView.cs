﻿using Code.Data;
using Code.Factories.Enemies;
using UnityEngine;
using Zenject;

namespace Code.Game.Hero
{
    public class HeroView : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private HeroRotation _rotation;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroHealth _health;

        private Transform _target;
        private IEnemiesPool _enemiesFactory;
        private float _minimalDistanceForAttack;

        [Inject]
        public void Constructor(IEnemiesPool enemiesFactory, GameData gameData)
        {
            _enemiesFactory = enemiesFactory;
            _minimalDistanceForAttack = gameData.MinimalDistanceForAttack;
        }

        private void LateUpdate()
        {
            _enemiesFactory.TryChangeTarget(_body.position, out _target, _minimalDistanceForAttack);

            if (_target == null)
                return;

            _rotation.Rotation(_body, _target);
            _attack.Attack(_health.Current, _body);
        }
    }
}