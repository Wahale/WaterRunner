using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text textScore,moneyText;
    [SerializeField] private MoveRoads moveRoads;

    private int meters = 0;
    public int money = 0;

    private float counter = 0f;
    private float speedPlayer;

    private void Start()
    {
        speedPlayer = GameObject.Find("RoadParent").GetComponent<MoveRoads>().speed;
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
            textScore.text =  meters.ToString() + ": Meters";
            moneyText.text = " money : " + money.ToString();
            if (meters % 20 == 0)
                moveRoads.speed += 0.1f;
        }
    }
}