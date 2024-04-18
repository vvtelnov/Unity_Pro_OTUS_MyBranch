using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private Vector3 _rotateDirection;
        
        [SerializeField] private int _hitPoints;
        [SerializeField] private bool _isDead;
        
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _rotateRate;
        [SerializeField] private bool _canRotate;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private bool _canFire;
        [SerializeField] private float _reloadTime;
        [SerializeField] private bool _isReloading;
        
        public bool FireRequest;
        private float _timer;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private RotateComponent _rotateComponent;

        private void Awake()
        {
            _moveComponent.AppendCondition(()=> !_isDead);
            
            _rotateComponent.AppendCondition(()=> !_isDead);
        }

        private void Update()
        {
            if (_isReloading)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _timer = 0;
                    _isReloading = false;
                }
            }
            
            if (_canFire && !_isReloading && FireRequest && !_isDead)
            {
                FireRequest = false;
                Fire();
            }
        }

        [Button]
        public void TakeDamage(int damage)
        {
            if (_isDead)
            {
                return;
            }
            
            _hitPoints -= damage;
            Debug.Log($"Take damage = {damage}");
            
            if (_hitPoints <= 0)
            {
                _isDead = true;
            }
        }

        public void Fire()
        {
            var bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            
            if (bullet.TryGetComponent(out MoveComponent moveComponent))
            {
                moveComponent.Direction = _firePoint.forward;
            }

            Debug.Log($"Fire!");
            _isReloading = true;
            _timer = _reloadTime;
        }
    }
}
