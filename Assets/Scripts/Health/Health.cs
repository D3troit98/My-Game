using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField]private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
       
        if(currentHealth > 0 )
        {
            //player got hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invurability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            //player dead
            if(!dead)
            {
               

                //player
                if(GetComponent<PlayerMovement>() != null )
                {
                    GetComponent<PlayerMovement>().enabled = false;
                }
               

                //Enemy
                if(GetComponentInParent<EnemyPatrol>() != null )
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }
               
                if(GetComponent<MeleEnemy>() != null )
                {
                    GetComponent<MeleEnemy>().enabled = false;
                }
                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                SoundManager.instance.PlaySound(deathSound);

            }
            
        }
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(Invurability());
        GetComponent<PlayerMovement>().enabled = true;
        GetComponentInParent<EnemyPatrol>().enabled = true;
        GetComponent<MeleEnemy>().enabled = true;
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth  + _value, 0, startingHealth);
    }

    private IEnumerator Invurability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        //waiting for sometime
        for (int i = 0;i < numberOfFlashes;i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
