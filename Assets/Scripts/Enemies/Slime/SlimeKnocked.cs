using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnocked : IState
{
    Slime slime;

    public SlimeKnocked( Slime slime )
    {
        this.slime = slime;
    }

    public void Enter()
    {
        // Does absolutely nothing
    }

    public void Execute()
    {
        // slime is going up. it can't change state yet
        if (slime.rb.velocity.y > 0) return;

        if (slime.periscope.IsTouchingLayers(LayerMask.GetMask("SolidGround")))
        {
            slime.StateMachine.ChangeState(new WanderSlime(slime, slime.path));
        }
    }

    public void Exit()
    {
        // does absolutely nothing
    }
}
