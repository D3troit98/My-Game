using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField]private float attackCooldown;
    [SerializeField]private int damage;
    [SerializeField] private float range;

    [Header("Attack Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Player Layer")]
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private LayerMask playerLayer;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;





    //references
    private Animator anim;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol= GetComponentInParent<EnemyPatrol>();
       
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack if the enemy sees the player
        if(PlayerinSight())
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                //Attack
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(attackSound);

            }
        }

        if(enemyPatrol!= null)
        {
            enemyPatrol.enabled = !PlayerinSight();
        }
        
    }

    private bool PlayerinSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,boxCollider.bounds.size.z) , 0, Vector2.left,0,playerLayer);

        if (hit.collider != null )
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerinSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
