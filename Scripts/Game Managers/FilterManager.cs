using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FilterManager : MonoBehaviour
{
    private static string SCAN_LINE_NUMBER = "_Number_Of_Scan_Lines";
    private static string PREF_KEY = "UseCRTFilter";

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
        LoadFilterPrefs();
    }

    private void SetCRTFilter()
    {
        _crtMaterial.SetFloat(SCAN_LINE_NUMBER, _scanLinesNumberCRT);
        _volume.sharedProfile = _crtProfile;
    }

    private void SetDefaultFilter()
    {
        _crtMaterial.SetFloat(SCAN_LINE_NUMBER, _scanLinesNumberDefault);
        _volume.sharedProfile = _defaultProfile;
    }

    public void ChangeFilter(bool isOn)
    {
        if (isOn)
            SetCRTFilter();
        else
            SetDefaultFilter();
        SaveFilterPrefs(isOn);
    }

    private void LoadFilterPrefs()
    {
        bool useCRT = PlayerPrefs.GetInt(PREF_KEY, 1) == 1;
        _toggle.isOn = useCRT;
        ChangeFilter(useCRT);
    }

    private void SaveFilterPrefs(bool useCRT)
    {
        PlayerPrefs.SetInt(PREF_KEY, useCRT ? 1 : 0);
        PlayerPrefs.Save();
    }
}
