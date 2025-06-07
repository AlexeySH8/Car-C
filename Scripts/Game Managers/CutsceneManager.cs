using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance { get; private set; }

    [SerializeField] private PlayableAsset _startCutscene;
    [SerializeField] private PlayableAsset _finishCutscene;

    public PlayableDirector _playableDirector;
    private bool _isSkipped;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _playableDirector = GetComponent<PlayableDirector>();
    }

    private IEnumerator PlayCutscene()
    {
        _isSkipped = false;
        UIManager.Instance.ShowSkipCutsceneButton();
        _playableDirector.Play();

        float timer = 0f;
        float duration = (float)_playableDirector.duration;
        while (timer < duration)
        {
            if (_isSkipped)
            {
                _playableDirector.time = _playableDirector.duration;
                _playableDirector.Evaluate();
                _playableDirector.Stop();
                UIManager.Instance.HideSkipCutsceneButton();
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }
        UIManager.Instance.HideSkipCutsceneButton();
    }

    public IEnumerator PlayStartCutscene()
    {
        _playableDirector.playableAsset = _startCutscene;
        yield return PlayCutscene();
    }

    public IEnumerator PlayFinishCutscene()
    {
        _playableDirector.playableAsset = _finishCutscene;
        yield return PlayCutscene();
    }

    public void SkipCutscene() => _isSkipped = true;
}
