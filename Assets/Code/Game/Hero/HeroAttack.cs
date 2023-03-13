using Code.Data;
using Code.Factories.Arrow;
using UnityEngine;
using Zenject;

namespace Code.Game.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _cooldown;

        private IArrowFactory _arrowFactory;
        private float _nextTimeAttack;
        private GameData _gameData;

        [Inject]
        public void Constructor(IArrowFactory arrowFactory, GameData gameData)
        {
            _arrowFactory = arrowFactory;
            _gameData = gameData;
        }

        public void Attack(Transform healthTransform, Transform body)
        {
            if (_nextTimeAttack > Time.time)
                return;

            _arrowFactory.Spawn(healthTransform, _gameData.HeroDamage, _spawnPoint.position, body.rotation,
                _gameData.ArrowSpeed);
            _nextTimeAttack = Time.time + _cooldown;
        }
    }
}