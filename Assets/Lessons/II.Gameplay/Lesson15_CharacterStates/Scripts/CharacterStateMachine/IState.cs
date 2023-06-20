namespace Lessons.CharacterStateMachine
{
    public interface IState
    {
        public void Enter();
        
        public void Exit();
    }
}