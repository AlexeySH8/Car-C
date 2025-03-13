using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance { get; private set; }

    public PlayableDirector PlayableDirector;
    [SerializeField] private PlayableAsset _startCutscene;
    [SerializeField] private PlayableAsset _finishCutscene;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        PlayableDirector = GetComponent<PlayableDirector>();
    }

    public void SetStartCutscene() => PlayableDirector.playableAsset = _startCutscene;

    public void SetFinishCutscene() => PlayableDirector.playableAsset = _finishCutscene;
}
