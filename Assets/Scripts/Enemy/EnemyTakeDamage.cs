using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        Vector3 closest = other.ClosestPointOnBounds(this.transform.position);
        Vector3 dir = (this.transform.position - closest).normalized;
        if (obj.CompareTag("Melee"))
        {
            MeleeAttackJudgement(dir,obj);
        }
    }

    private void MeleeAttackJudgement(Vector3 direction,in GameObject player)
    {
        PlayerStateManager state = player.GetComponent<PlayerStateManager>();
        if(state == null)
        {
            return;
        }
        if(state.CurrentPlayerState == PlayerState.e_PlayerState.LeftAttack)
        {
            Debug.Log("ãﬂê⁄ãÚÇÁÇ¡ÇΩ");
        }
    }
}
