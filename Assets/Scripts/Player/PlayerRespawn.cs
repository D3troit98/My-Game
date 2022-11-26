using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        if(currentCheckpoint == null)
        {
            //show game over screen
            uiManager.GameOver();
            return;
        }
        else
        {
            transform.position = currentCheckpoint.position;
            playerHealth.Respawn();
            Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; 
            collision.GetComponent<Animator>().SetTrigger("appear");

           
        }
        
    }


}
