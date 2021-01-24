using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField]
    private GameObject _deactiveHandle, _activeHandle, _deactiveSlider, _activeSlider;
    private Toggle _toggle;
    private bool _activationState;

    private void Start()
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
