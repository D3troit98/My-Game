using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{

    [SerializeField] private AudioClip doorSound;
    private int scene;

    private void Update()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(scene + 1);
            SoundManager.instance.PlaySound(doorSound);

        }

    }
}