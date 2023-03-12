using UnityEngine;

namespace Code.Game
{
    public class HeroRotation : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        
        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            Rotation();
        }

        public void ChangeTarget(Transform target) => 
            _target = target;

        private void Rotation()
        {
            if(_target == null)
                return;

            _body.rotation = Quaternion.LookRotation(transform.forward, _target.transform.position);
        }
    }
}