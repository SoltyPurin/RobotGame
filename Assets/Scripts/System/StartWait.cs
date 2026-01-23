using System.Collections;
using UnityEngine;

public class StartWait : MonoBehaviour
{
    [SerializeField, Header("最初のアニメーションが終わるまでの時間")]
    private float _animationTime = 2.4f;

    private void Awake()
    {
        PlayerInputManager input = GameObject.FindAnyObjectByType<PlayerInputManager>();
        var aiCOntroller = FindObjectsByType<TestAIController>(FindObjectsSortMode.None);
        var stackDetect  = FindObjectsByType<EnemyStackDetect>(FindObjectsSortMode.None);
        input.enabled = false;
        foreach(var ai in aiCOntroller)
        {
            ai.enabled = false;
        }
        foreach(var stack in stackDetect)
        {
            stack.enabled = false;
        }

        StartCoroutine(EnableProtocol(input,aiCOntroller,stackDetect));
    }

    private IEnumerator EnableProtocol(PlayerInputManager input, TestAIController[] controllers,EnemyStackDetect[] stacks)
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
    }
}
