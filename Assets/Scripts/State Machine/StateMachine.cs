public class StateMachine<T> where T : StateInput
{
    public State<T> State { get; private set; }
    public T Input { get; private set; }

    public void Initialize(T input, State<T> initState)
    {
        Input = input;
        SetState(initState);
    }

    public void SetState(State<T> newState)
    {
        State?.Exit(Input);

        State = newState;
        State.Enter(Input);
    }

    public void Update()
    {
        State?.Update(Input);
    }
}
