using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftedge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("idle behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField]private Animator anim;


    private void Awake()
    {
        
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if(enemy.position.x >= leftedge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                //Change directiom
                DirectionChange();
            }
            
        }
        else
        {   
            if(enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }   
           
        }
       
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
       
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        //Make enemy face direction first
        enemy.localScale = new Vector3(Mathf.Abs( initScale.x) * _direction, initScale.y, initScale.z);

        //Move in the direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime *  _direction * speed, enemy.position.y, enemy.position.z);
    }
}
