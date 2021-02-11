using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private GameObject _deactiveHandle, _activeHandle, _deactiveSlider, _activeSlider;
    public Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    public void Toggle()
    {
            _deactiveHandle.SetActive(!_toggle.isOn);
            _deactiveSlider.SetActive(!_toggle.isOn);
            _activeHandle.SetActive(_toggle.isOn);
            _activeSlider.SetActive(_toggle.isOn);
    }
}
