using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    
    private float _lastDashTime;
    
    private int _dashDirection;
    
    private Vector2 _lastAfterImagePosition;
    public PlayerDashState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (Core.CollisionSenses.Wall)
        {
            Core.Movement.Flip();
        }
        
        CanDash = false;
        Player.InputHandler.UseDashInput();
        
        //Debug.LogError("helo");
        
        //_dashDirection = new Vector2(Core.Movement.FacingDirection, 0);
        //Debug.Log(Player.RB.gravityScale);
        
        Core.Movement.RB.gravityScale = 0;
        
        //Debug.Log(Player.RB.gravityScale);
        
        IsAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
        
        Core.Movement.RB.gravityScale = PlayerData.gravity;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _dashDirection = Core.Movement.FacingDirection;
        
        if (!IsExitingState)
        {
            Player.Animator.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);
            Player.Animator.SetFloat("xVelocity", Mathf.Abs(Core.Movement.CurrentVelocity.x));
            
            // if (_wallDash)
            // {
            //     Core.Movement.WallDashFlip();
            //     Core.Movement.SetDashVelocity(PlayerData.dashVelocity, _dashDirection);
            // }
            // else
            // {
            //     Core.Movement.SetDashVelocity(PlayerData.dashVelocity, _dashDirection);
            // }
            
            PlaceAfterImage();
            
            Core.Movement.SetDashVelocity(PlayerData.dashVelocity, _dashDirection);
            CheckIfShouldPlaceAfterImage();
            
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
    
    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        _lastAfterImagePosition = Player.transform.position;
    }
    
    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(Player.transform.position, _lastAfterImagePosition) >= PlayerData.distanceBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }
}
