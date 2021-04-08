using UnityEngine;

public class GoToEnd : MonoBehaviour
{
    public MoveRoads moveRoads;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("road"))
        {
            SetNewPosition(ref moveRoads.roads, other);
            ChangeArrayOrder(ref moveRoads.roads, moveRoads.roads.Length);
        }
    }

    public static void ChangeArrayOrder(ref GameObject[] roads, int size)
    {
        int i;
        var temp = roads[0];
        for (i = 0, size--; i < size; i++)
            roads[i] = roads[i + 1];
        roads[size] = temp;
    }

    private void SetNewPosition(ref GameObject[] roads, Collider other) => other.transform.parent.transform.position = roads[roads.Length - 1].transform.GetChild(1).transform.position;
}
