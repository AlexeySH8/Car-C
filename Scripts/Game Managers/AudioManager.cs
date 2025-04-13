using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip[] _nightMusics;
    [SerializeField] private AudioClip[] _morningMusics;
    [SerializeField] private AudioClip[] _dayMusics;
    [SerializeField] private AudioClip _cardSelectionMusic;
    [SerializeField] private AudioClip _clickSFX;
    [SerializeField] private AudioClip _jumpSFX;
    [SerializeField] private AudioClip _explosionSFX;
    [SerializeField] private AudioClip _HPRecoverySFX;
    [SerializeField] private AudioClip _jumpPowerupSFX;
    [SerializeField] private AudioClip _speedPowerupSFX;
    [SerializeField] private AudioClip _timeDilationSFX;
    [SerializeField] private AudioClip _textTypewriterSFX;
    private Dictionary<byte, AudioClip[]> _musicDic;
    private readonly string _musicVolumeString = "musicVolume";
    private readonly string _sfxVolumeString = "sfxVolume";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _musicDic = new Dictionary<byte, AudioClip[]>
        {
            {0,_nightMusics },
            {1,_morningMusics},
            {2,_dayMusics}
        };
        LoadVolumePrefs();
    }

    private void Start()
    {
        CardManager.Instance.OnStartCardSelection += PlayStartCardSelectionSFX;
        CardManager.Instance.OnFinishCardSelection += PlayMusic;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += PlayGameStartSFX;
        GameManager.Instance.OnGameOver += _musicSource.Stop;
        GameManager.Instance.OnGamePaused += _musicSource.Pause;
        GameManager.Instance.OnGameContinued += _musicSource.Play;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= PlayGameStartSFX;
        GameManager.Instance.OnGameOver -= _musicSource.Stop;
        GameManager.Instance.OnGamePaused -= _musicSource.Pause;
        GameManager.Instance.OnGameContinued -= _musicSource.Play;
        CardManager.Instance.OnStartCardSelection -= PlayStartCardSelectionSFX;
        CardManager.Instance.OnFinishCardSelection -= PlayMusic;
    }

    private void PlayGameStartSFX()
    {
        _sfxSource.Stop();
        PlayMusic();
    }

    public void PlayStartCardSelectionSFX()
    {
        _musicSource.Stop();
        _musicSource.clip = _cardSelectionMusic;
        _musicSource.Play();
    }

    private void PlayMusic()
    {
        AudioClip[] currentCityPlaylist = _musicDic[RepeatBackground.CurrentCity];
        AudioClip randomMusic = currentCityPlaylist[Random.Range(0, currentCityPlaylist.Length)];
        _musicSource.clip = randomMusic;
        _musicSource.Play();
    }

    private IEnumerator PlayExplosionSFXCourutine()
    {
        var minPitch = 0.5f;
        var maxPitch = 2f;
        _sfxSource.pitch = Random.Range(minPitch, maxPitch);
        _sfxSource.PlayOneShot(_explosionSFX);
        yield return new WaitForSeconds(_explosionSFX.length);
        _sfxSource.pitch = 1f;
    }

    public void ChangeMusicVolume()
    {
        _musicSource.volume = _musicSlider.value;
        SaveVolumePrefs();
    }

    public void ChangeSFXVolume()
    {
        _sfxSource.volume = _sfxSlider.value;
        SaveVolumePrefs();
    }

    private void SaveVolumePrefs()
    {
        PlayerPrefs.SetFloat(_musicVolumeString, _musicSlider.value);
        PlayerPrefs.SetFloat(_sfxVolumeString, _sfxSlider.value);
        PlayerPrefs.Save();
    }

    private void LoadVolumePrefs()
    {
        var currentMusicValue = PlayerPrefs.GetFloat(_musicVolumeString, 1f);
        var currentSFXValue = PlayerPrefs.GetFloat(_sfxVolumeString, 1f);
        _musicSlider.value = currentMusicValue;
        _sfxSlider.value = currentSFXValue;
        _musicSource.volume = _musicSlider.value;
        _sfxSource.volume = _sfxSlider.value;
    }

    public void PlayExplosionSFX() => StartCoroutine(PlayExplosionSFXCourutine());

    public void PlayClickSFX() => _sfxSource.PlayOneShot(_clickSFX);

    public void PlayJumpSFX() => _sfxSource.PlayOneShot(_jumpSFX);

    public void PlayHPRecoverySFX() => _sfxSource.PlayOneShot(_HPRecoverySFX);

    public void PlayJumpPowerupSFX() => _sfxSource.PlayOneShot(_jumpPowerupSFX);

    public void PlaySpeedPowerupSFX() => _sfxSource.PlayOneShot(_speedPowerupSFX);

    public void PlayTimeDilationSFX() => _sfxSource.PlayOneShot(_timeDilationSFX);

    public void PlayTextTypewriterSFX() => _sfxSource.PlayOneShot(_textTypewriterSFX);
}
