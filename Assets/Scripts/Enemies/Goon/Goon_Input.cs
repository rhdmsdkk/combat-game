public class Goon_Input : StateInput
{
    public StateMachine<Goon_Input> stateMachine;
    public Goon goon;
    public Player player;

    public Goon_Input(StateMachine<Goon_Input> stateMachine, Goon goon, Player player)
    {
        this.stateMachine = stateMachine;
        this.goon = goon;
        this.player = player;
    }
}