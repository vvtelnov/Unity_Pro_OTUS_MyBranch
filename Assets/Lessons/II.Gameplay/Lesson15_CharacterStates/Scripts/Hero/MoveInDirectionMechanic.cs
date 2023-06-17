using Declarative;
using Elementary;
using Game.GameEngine;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.Hero
{
    public sealed class MoveInDirectionMechanic : IFixedUpdateListener
    {
        private IValue<IWalkableSurface> _surface;

        private IMoveInDirectionMotor _moveMotor;
        private ITransformEngine _transformEngine;

        private IValue<float> _speed;

        public void Construct(IValue<IWalkableSurface> surface, ITransformEngine transformEngine,
            IMoveInDirectionMotor moveMotor, IValue<float> speed)
        {
            _surface = surface;
            _transformEngine = transformEngine;
            _moveMotor = moveMotor;
            _speed = speed;
        }

        void IFixedUpdateListener.FixedUpdate(float deltaTime)
        {
            var velocity = _moveMotor.Direction * (_speed.Current * deltaTime);

            if (_surface.Current != null)
            {
                MoveBySurface(velocity);
            }
            else
            {
                _transformEngine.MovePosition(velocity);
            }
        }
        
        private void MoveBySurface(Vector3 velocity)
        {
            var nextPosition = _transformEngine.WorldPosition + velocity;
            var currentSurface = _surface.Current;
            
            if (currentSurface.IsAvailablePosition(nextPosition))
            {
                _transformEngine.SetPosiiton(nextPosition);
            }
            else if (currentSurface.FindAvailablePosition(nextPosition, out var clampedPosition))
            {
                _transformEngine.SetPosiiton(clampedPosition);
            }
        }
    }
}