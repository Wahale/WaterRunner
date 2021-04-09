using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text textScore;

    private int meters = 0;
    private float counter = 0f;
    private float speedPlayer;
    private string _text = ": Meters";

    private void Start()
    {
        textScore.GetComponent<Text>();
        speedPlayer = GameObject.FindGameObjectWithTag("Speed").GetComponent<MoveRoads>().speed;
    }

    private void Update()
    {
        Score_(speedPlayer);
    }

    // Новая скорость
    public void NewSpeed(float newSpeed)
    {
        speedPlayer = newSpeed;
    }

    private void Score_(float speed)
    {
        counter += Time.deltaTime * speed;

        if (counter >= 1)
        {
            counter = 0f;
            meters++;
            textScore.text = meters.ToString() + _text;
        }
    }
}