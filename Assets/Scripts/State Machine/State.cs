public abstract class State<T> where T : StateInput
{
    public abstract void Enter(T input);
    public abstract void Update(T input);
    public abstract void FixedUpdate(T input);
    public abstract void Exit(T input);
}

public abstract class StateInput { }