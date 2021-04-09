using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]private PlayerAnimationController animator=default;
    [SerializeField]private LayerMask ObstacleLayer;
    [SerializeField]private LayerMask WaterLayer;


    private void Awake()
    {
        this.animator = this.GetComponent<PlayerAnimationController>();
        if (this.animator == null) Debug.LogError("Player Animator is null at PlayerDeath Script");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == ObstacleLayer) this.animator?.DeathObstacle();
        if (collision.gameObject.layer == WaterLayer) this.animator?.DeathWater(); 
    }
}
