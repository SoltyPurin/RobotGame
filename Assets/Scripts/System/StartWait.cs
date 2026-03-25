using System.Collections;
using UnityEngine;

public class StartWait : MonoBehaviour
{
    [SerializeField, Header("最初のアニメーションが終わるまでの時間")]
    private float _animationTime = 2.4f;
    [SerializeField, Header("ユーザーのキャンバス")]
    private CanvasGroup _userCanvas = default;
    [SerializeField,Header("練習中か？")]
    private bool _isPractice = false;

    private void Awake()
    {
        PlayerInputManager input = FindAnyObjectByType<PlayerInputManager>();
        DeathManager deathManager = FindAnyObjectByType<DeathManager>();
        TestAIController[] aiCOntroller = FindObjectsByType<TestAIController>(FindObjectsSortMode.None);
        EnemyStackDetect[] stackDetect  = FindObjectsByType<EnemyStackDetect>(FindObjectsSortMode.None);
        TakeDamageScript[] takeDamages = FindObjectsByType<TakeDamageScript>(FindObjectsSortMode.None);
        input.enabled = false;
        if (!_isPractice)
        {
            deathManager.enabled = false;
        }
        _userCanvas.alpha = 0;
        foreach (var ai in aiCOntroller)
        {
            ai.enabled = false;
        }
        foreach(var stack in stackDetect)
        {
            stack.enabled = false;
        }
        foreach (var takeDamage in takeDamages)
        {
            takeDamage.enabled = false;
        }
        StartCoroutine(EnableProtocol(input,aiCOntroller,stackDetect,takeDamages,deathManager));
    }

    private IEnumerator EnableProtocol(PlayerInputManager input,
        TestAIController[] controllers,EnemyStackDetect[] stacks,
        TakeDamageScript[] takeDamages,DeathManager deathManager )
    {
        yield return new WaitForSeconds(_animationTime);
        input.enabled = true;
        foreach(var controller in controllers)
        {
            controller.enabled = true;
        }
        foreach(var enStack in stacks)
        {
            enStack.enabled = true;
        }
        foreach (var takeDamage in takeDamages)
        {
            takeDamage.enabled =true;
        }
        if (!_isPractice)
        {
            deathManager.enabled = true;
        }
        _userCanvas.alpha = 1;
    }
}
