using System.Collections;
using UnityEngine;

public class StartWait : MonoBehaviour
{
    [SerializeField, Header("最初のアニメーションが終わるまでの時間")]
    private float _animationTime = 2.4f;
    [SerializeField, Header("ユーザーのキャンバス")]
    private GameObject _userCanvas = default;

    private void Awake()
    {
        PlayerInputManager input = FindAnyObjectByType<PlayerInputManager>();
        var aiCOntroller = FindObjectsByType<TestAIController>(FindObjectsSortMode.None);
        var stackDetect  = FindObjectsByType<EnemyStackDetect>(FindObjectsSortMode.None);
        var takeDamages = FindObjectsByType<TakeDamageScript>(FindObjectsSortMode.None);
        input.enabled = false;
        _userCanvas.SetActive(false);
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
        StartCoroutine(EnableProtocol(input,aiCOntroller,stackDetect,takeDamages));
    }

    private IEnumerator EnableProtocol(PlayerInputManager input,
        TestAIController[] controllers,EnemyStackDetect[] stacks,
        TakeDamageScript[] takeDamages )
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
        _userCanvas.SetActive(true);
    }
}
