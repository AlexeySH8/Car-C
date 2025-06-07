using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FilterManager : MonoBehaviour
{
    private static string SCAN_LINE_NUMBER = "_Number_Of_Scan_Lines";

    [SerializeField] private Toggle _toggle;
    [SerializeField] private Material _crtMaterial;
    [SerializeField] private VolumeProfile _crtProfile;
    [SerializeField] private VolumeProfile _defaultProfile;

    private float _scanLinesNumberCRT = 200f;
    private float _scanLinesNumberDefault = 0f;
    private Volume _volume;

    private void Awake()
    {
        _volume = GetComponent<Volume>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            SetCRTFilter();
        if (Input.GetKeyDown(KeyCode.F))
            SetDefaultFilter();
    }

    public void SetCRTFilter()
    {
        _crtMaterial.SetFloat(SCAN_LINE_NUMBER, _scanLinesNumberCRT);
        _volume.sharedProfile = _crtProfile;
    }

    public void SetDefaultFilter()
    {
        _crtMaterial.SetFloat(SCAN_LINE_NUMBER, _scanLinesNumberDefault);
        _volume.sharedProfile = _defaultProfile;
    }

    public void ChangeFilter(bool isOn)
    {
        Debug.Log(isOn);
        if (isOn)
            SetCRTFilter();
        else
            SetDefaultFilter();
    }
}
