using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField]
    private GameObject deactiveHandle, activeHandle, deactiveSlider, activeSlider;
    private Toggle toggle;
    private bool activationState;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void Toggle()
    {
            deactiveHandle.SetActive(!toggle.isOn);
            deactiveSlider.SetActive(!toggle.isOn);
            activeHandle.SetActive(toggle.isOn);
            activeSlider.SetActive(toggle.isOn);
    }
}
