using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator=default;
    [SerializeField] private string LeftMoveAnimationName;
    [SerializeField] private string RightMoveAnimationName;
    [SerializeField] private string JumpAnimationName;
    [SerializeField] private string DashAnimationName;
    [SerializeField] private string DeathObstacleAnimationName;
    [SerializeField] private string DeathWaterAnimationName;


    public void MoveLeft() =>animator?.SetTrigger(LeftMoveAnimationName);
    public void MoveRight() => animator?.SetTrigger(RightMoveAnimationName);
    public void Jump() => animator?.SetTrigger(JumpAnimationName);
    public void Dash() => animator?.SetTrigger(DashAnimationName);
    public void DeathObstacle() => animator?.SetTrigger(DeathObstacleAnimationName);
    public void DeathWater() => animator?.SetTrigger(DeathWaterAnimationName);
}
