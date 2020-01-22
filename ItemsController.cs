using UnityEngine;
using UnityEngine.SceneManagement;

//Class ItemsController, to control items behaviour.
public class ItemsController : MonoBehaviour
{
    //On Update, move the item (fish or obstacle).
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Menu") && transform.CompareTag("Fish"))
        {
            transform.position -= transform.up * Time.deltaTime;
            
        }
        else
            transform.position += new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * GameController.instance.velocity;
    }

    //On trigger enter, if collision is between a fish and a rock, and scene is not menu, destroy the fish.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.CompareTag("Fish") && collision.transform.CompareTag("Obstacle") && !SceneManager.GetActiveScene().name.Equals("Menu"))
            Destroy(gameObject);
    }

    //On became invisible (out of screen), destroy itself.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
