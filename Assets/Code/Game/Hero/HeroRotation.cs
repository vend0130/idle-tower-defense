using Code.Data;
using Code.Extensions;
using Code.Factories.Enemies;
using UnityEngine;
using Zenject;

namespace Code.Game.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        [SerializeField] private Transform _body;

        private Transform _target;
        private IEnemiesPool _enemiesFactory;
        private float _minimalDistanceForAttack;

        [Inject]
        public void Constructor(IEnemiesPool enemiesFactory, GameData gameData)
        {
            _enemiesFactory = enemiesFactory;
            _minimalDistanceForAttack = gameData.MinimalDistanceForAttack;
        }

        private void FixedUpdate() =>
            _enemiesFactory.TryChangeTarget(_body.position, _minimalDistanceForAttack, out _target);

        private void LateUpdate() =>
            Rotation();

        private void Rotation()
        {
            if (_target == null)
                return;

            _body.LookAt2D(_target.position);
        }
    }
}