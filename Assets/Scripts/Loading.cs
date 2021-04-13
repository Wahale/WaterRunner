using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject panelDownload;

    [SerializeField] private float timerDownload;
    private bool load = true;

    private void Awake()
    {
        panelDownload.SetActive(true);
    }

    private void Update()
    {
        if (load)
        {
            timerDownload -= Time.deltaTime;

            if (timerDownload <= 0)
            {
                panelDownload.SetActive(false);
                load = false;
            }
        }
    }
}