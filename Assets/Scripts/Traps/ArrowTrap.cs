using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField]private float attakCooldown;
    [SerializeField]private Transform firepoint;
    [SerializeField] private GameObject[] arrows;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private float cooldownTimer;
    private void Attack()
    {
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(arrowSound);
        arrows[FindArrow()].transform.position = firepoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        for(int i = 0; i< arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attakCooldown)
        {
            Attack();
        }
    }
}
