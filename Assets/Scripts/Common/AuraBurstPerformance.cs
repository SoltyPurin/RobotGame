using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class AuraBurstPerformance : MonoBehaviour
{
    [SerializeField, Header("メインのカメラ")]
    private CinemachineCamera _mainCamera = default;
    [SerializeField, Header("バースト発動時のカメラ")]
    private CinemachineCamera _burstCamera = default;
    [SerializeField,Header("バースト発動したやつのどれくらい前の座標に行くか")]
    private Vector3 _positionOffset = Vector3.zero;
    [SerializeField, Header("バースト発動の音")]
    private AudioClip _burstSound = default;

    private UIViewer _ui = default;
    private AudioSource _audioSource = default;

    private bool _isBurstPerformancing = false;
    public bool IsBurstPerformancing
    {
        get { return _isBurstPerformancing; }   
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _ui = FindAnyObjectByType<UIViewer>();
    }

    public void AuraBurstCutIn(GameObject burstUser)
    {
        _isBurstPerformancing = true;
        Animator animator = burstUser.GetComponentInChildren<Animator>();
        animator.speed = 0;
        var childrens = burstUser.GetComponentsInChildren<Transform>();
        GameObject head = null;
        foreach (var child in childrens)
        {
            if (child.tag == "Head")
            {
                head = child.gameObject;
            }
        }
        transform.parent = head.transform;
        transform.localPosition = _positionOffset;
        transform.LookAt(head.transform);
        _mainCamera.Priority = 0;
        _burstCamera.Priority = 1;
        _audioSource.PlayOneShot(_burstSound);
        StartCoroutine(CutInEnd(animator));
        _ui.BurstStart();
        Time.timeScale = 0;
    }

    private IEnumerator CutInEnd(Animator animator)
    {
        yield return new WaitForSecondsRealtime(2);
        _isBurstPerformancing = false;
        animator.speed = 1;
        transform.parent = null;
        Time.timeScale = 1;
        _mainCamera.Priority = 1;
        _burstCamera.Priority=0;
        _ui.BurstEnd();
    }
}
