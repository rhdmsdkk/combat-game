using UnityEngine;

public class Player_Dead : State<Player_Input>
{
    public override void Enter(Player_Input input)
    {
        input.player.gameObject.SetActive(false);
        input.player.enabled = false;
    }

    public override void Update(Player_Input input) { }

    public override void Exit(Player_Input input) { }

    public override void FixedUpdate(Player_Input input) { }
}
