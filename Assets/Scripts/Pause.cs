using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Pause : MonoBehaviour
{

    [SerializeField] private Image pausePanel;

    private Button pauseButton;
    
    private GameObject player;

    private void Awake()
    {
        pauseButton = gameObject.GetComponent<Button>();
        pauseButton.onClick.AddListener(PauseGame);
        pausePanel.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void PauseGame()
    {
        if (HungryLouse.Pause)
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
        player.GetComponent<LouseMovement>().enabled = true;
        pausePanel.gameObject.SetActive(false);
        HungryLouse.Pause = false;
    }

    private void Stop()
    {
        player.GetComponent<LouseMovement>().enabled = false;
        pausePanel.gameObject.SetActive(true);
        HungryLouse.Pause = true;
    }

}
