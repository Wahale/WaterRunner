using UnityEngine;

public class MoveRoads : MonoBehaviour
{
    public GameObject[] roads;
    public float speed;

    private void Start() => roads = GameObject.FindGameObjectsWithTag("Platform");

    private void FixedUpdate()=> MoveRoad();

    private void MoveRoad()
    {
        for (int i = 0; i < roads.Length; i++)
        {
            roads[i].transform.Translate(0, 0, -speed * Time.fixedDeltaTime);
        }
    }
}
