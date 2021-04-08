using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public Transform roadParent;
    public List<GameObject> roads = new List<GameObject>();
    public GameObject previousPosition;
    public Queue<GameObject> objects = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < roads.Count; i++)
        {
            var position = previousPosition.transform.GetChild(1).transform.position;
            var nextRoad = Instantiate(roads[i], position, Quaternion.identity, roadParent);
            previousPosition = nextRoad;
        }
    }
}
