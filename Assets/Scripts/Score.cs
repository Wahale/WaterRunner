using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text textScore;

    private int meters = 0;
    public int money = 0;

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
    // Подбор монет
    public void Money(int _money)
    {
        money = _money;
    }

    private void Score_(float speed)
    {
        counter += Time.deltaTime * speed;

        if (counter >= 1)
        {
            counter = 0f;
            meters++;
            textScore.text = money.ToString() + " money : " + meters.ToString() + _text;
        }
    }
}