using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    
    private float _lastDashTime;
    
    private int _dashDirection;
    public PlayerDashState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (Core.CollisionSenses.Wall)
        {
            Core.Movement.WallDashFlip();
        }
        
        CanDash = false;
        Player.InputHandler.UseDashInput();
        
        Debug.LogError("helo");
        
        //_dashDirection = new Vector2(Core.Movement.FacingDirection, 0);
        //Debug.Log(Player.RB.gravityScale);
        
        Player.RB.gravityScale = 0;
        
        //Debug.Log(Player.RB.gravityScale);
        
        IsAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
        
        Player.RB.gravityScale = PlayerData.gravity;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _dashDirection = Core.Movement.FacingDirection;
        
        if (!IsExitingState)
        {
            // if (_wallDash)
            // {
            //     Core.Movement.WallDashFlip();
            //     Core.Movement.SetDashVelocity(PlayerData.dashVelocity, _dashDirection);
            // }
            // else
            // {
            //     Core.Movement.SetDashVelocity(PlayerData.dashVelocity, _dashDirection);
            // }
            
            Core.Movement.SetDashVelocity(PlayerData.dashVelocity, _dashDirection);
            //Player.RB.drag = PlayerData.dashDrag;
            
            if (Time.time >= StartTime + PlayerData.dashTime || Core.CollisionSenses.Wall)
            {
                //Player.RB.drag = 0;
                IsAbilityDone = true;
                _lastDashTime = Time.time;
            }
        }
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + PlayerData.dashCoolDown;
    }
    
    public void ResetDash() => CanDash = true;
}
