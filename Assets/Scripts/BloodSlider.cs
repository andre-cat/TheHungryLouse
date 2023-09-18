using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BloodSlider : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        slider.value = HungryLouse.Blood;
    }

    void Update()
    {
        SetBlood();
    }

    private void SetBlood()
    {
        slider.value = HungryLouse.Blood;
    }
}
