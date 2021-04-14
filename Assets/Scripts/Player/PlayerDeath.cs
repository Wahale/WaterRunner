using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController animator = default;
    [SerializeField] private LayerMask ObstacleLayer;
    [SerializeField] private LayerMask WaterLayer;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private MoveRoads moveRoads;


    private void Awake()
    {
        this.animator = this.GetComponent<PlayerAnimationController>();
        if (this.animator == null) Debug.LogError("Player Animator is null at PlayerDeath Script");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Mathf.Pow(2, collision.gameObject.layer) == ObstacleLayer.value)
        {
            this.animator?.DeathObstacle();
            //gameObject.GetComponent<Rigidbody>().useGravity = false;
            moveRoads.speed = 0;
            deathPanel?.SetActive(true);
        }
        if (Mathf.Pow(2, collision.gameObject.layer) == WaterLayer.value)
        {
            this.animator?.DeathWater();
            //gameObject.GetComponent<Rigidbody>().useGravity = false;
            moveRoads.speed = 0;
            deathPanel?.SetActive(true);
        }
    }
}
