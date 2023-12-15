using UnityEngine;

public class Slideshow : MonoBehaviour
{

    [SerializeField] private GameObject[] slides;

    private int currentSlide;

    private void Start()
    {
        currentSlide = 0;
        slides[currentSlide].SetActive(true);
    }

    public void ShowNext()
    {
        currentSlide += 1;
        slides[currentSlide].SetActive(true);
        slides[currentSlide - 1].SetActive(false);
    }

    public void ShowPrev()
    {
        currentSlide -= 1;
        slides[currentSlide].SetActive(true);
        slides[currentSlide + 1].SetActive(false);
    }
}
