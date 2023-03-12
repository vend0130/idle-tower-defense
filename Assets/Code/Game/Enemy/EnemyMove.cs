using Code.Extensions;
using Code.Game.Hero;
using UnityEngine;
using Zenject;

namespace Code.Game.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private Transform _current;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _minimalDistanceToHero = .75f;
        
        private Transform _target;

        [Inject]
        public void Constructor(HeroRotation hero)
        {
            _target = hero.transform;
        }

        private void Update()
        {
            if (_target == null)
                return;
            
            Move();
            Rotation();
        }

        private void Move()
        {
            float distance = Vector2.Distance(_current.position, _target.position);
            
            if(HeroNotReached(distance))
                return;
            
            float moveTime = _speed / distance * Time.deltaTime;
            _current.position = Vector2.Lerp(_current.position, _target.position, moveTime);
        }

        private bool HeroNotReached(float distance) => 
            distance <= _minimalDistanceToHero;

        private void Rotation()
        {
            _current.LookAt2D(_target);
        }
    }
}