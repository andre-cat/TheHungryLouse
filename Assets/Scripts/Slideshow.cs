using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Slideshow : MonoBehaviour
{

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject nextPanel;

    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ShowNext);
    }

    private void ShowNext()
    {
        nextPanel.SetActive(true);
        panel.SetActive(false);
    }
}
