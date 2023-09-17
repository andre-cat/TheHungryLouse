using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Pause : MonoBehaviour
{

    [SerializeField] private Image pausePanel;

    private Button pauseButton;

    private void Awake()
    {
        pauseButton = gameObject.GetComponent<Button>();
        pauseButton.onClick.AddListener(PauseGame);
        pausePanel.gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        if (HungryLouse.pause)
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
        HungryLouse.pause = false;
        Time.timeScale = 1f;
        pausePanel.gameObject.SetActive(false);
    }

    private void Stop()
    {
        HungryLouse.pause = true;
        Time.timeScale = 0f;
        pausePanel.gameObject.SetActive(true);
    }

}
