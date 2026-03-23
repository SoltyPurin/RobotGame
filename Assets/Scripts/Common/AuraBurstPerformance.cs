using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using System.Collections.Generic;

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

    private TestAIController[] _aiControllers = new TestAIController[2];
    private GameObject _playerObj = default;
    private PlayerMove _playerMove = default;
    private PlayerInputManager _input = default;

    private bool _isBurstPerformancing = false;
    public bool IsBurstPerformancing
    {
        get { return _isBurstPerformancing; }   
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _ui = FindAnyObjectByType<UIViewer>();
        _aiControllers = FindObjectsByType<TestAIController>(FindObjectsSortMode.None);
        _playerObj = GameObject.FindWithTag("Player");
        _playerMove = _playerObj.GetComponent<PlayerMove>();
        _input = _playerObj.GetComponent<PlayerInputManager>();
    }

    public void AuraBurstCutIn(GameObject burstUser)
    {
        _isBurstPerformancing = true;
        Animator animator = burstUser.GetComponentInChildren<Animator>();
        animator.speed = 0;
        var childrens = burstUser.GetComponentsInChildren<Transform>();
        GameObject head = null;
        foreach (Transform child in childrens)
        {
            if (child.tag == "Head")
            {
                head = child.gameObject;
            }
        }
        foreach(TestAIController controller in _aiControllers)
        {
            if(controller != null)
            {
                controller.InBurstMove(_isBurstPerformancing);
            }
        }
        _playerMove.InBurstMove(_isBurstPerformancing);
        _input.InBurstMove(_isBurstPerformancing);
        transform.parent = head.transform;
        transform.localPosition = _positionOffset;
        transform.LookAt(head.transform);
        _mainCamera.Priority = 0;
        _burstCamera.Priority = 1;
        _audioSource.PlayOneShot(_burstSound);
        StartCoroutine(CutInEnd(animator));
        _ui.BurstStart();
    }

    private IEnumerator CutInEnd(Animator animator)
    {
        yield return new WaitForSecondsRealtime(2);
        _isBurstPerformancing = false;
        animator.speed = 1;
        foreach (TestAIController controller in _aiControllers)
        {
            if (controller != null)
            {
                controller.InBurstMove(_isBurstPerformancing);
            }
        }

        _playerMove.InBurstMove(_isBurstPerformancing);
        _input.InBurstMove(_isBurstPerformancing);
        transform.parent = null;
        _mainCamera.Priority = 1;
        _burstCamera.Priority=0;
        _ui.BurstEnd();
    }
}
