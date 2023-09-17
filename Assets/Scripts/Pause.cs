using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    [SerializeField] private Image pausePanel;

    private readonly bool isPause = false;

    private void Awake()
    {
        pausePanel.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        if (isPause)
        {
            Continue();
        }
        else
        {
            Stop();
        }
    }

    private void Continue()
    {
        Time.timeScale = 1f;
        pausePanel.gameObject.SetActive(false);
    }

    private void Stop()
    {
        Time.timeScale = 0f;
        pausePanel.gameObject.SetActive(true);
    }
}
