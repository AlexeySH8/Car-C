using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManPosition : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _startPlayerManPosition = new Vector3(-369.8f, 13, 58.3f);
    private Vector3 _finishPlayerManPosition = new Vector3(1007, 13, 11);

    private void Start()
    {
        _animator = GetComponent<Animator>();
        ResetPosition();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnFinishGame += SetFinishPosition;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnFinishGame -= SetFinishPosition;
    }

    private void ResetPosition()
    {
        _animator.SetBool("IsFinishGame", false);
        transform.position = _startPlayerManPosition;
    }

    private void SetFinishPosition() => StartCoroutine(SetFinishPositionCourutine());

    private IEnumerator SetFinishPositionCourutine()
    {
        _animator.SetBool("IsFinishGame", true);
        _animator.CrossFade("Happy", 0.1f);
        yield return new WaitForSeconds(5f);
        transform.position = _finishPlayerManPosition;
    }
}
