using UnityEngine;

//Class BoatController, to control Boat's behaviour.
public class BoatController : MonoBehaviour
{
    //Variables.
    private Rigidbody2D rb;
    public float force;

    //On start, get the rigidbody and ignore some collisions.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        //Ignore collision between fish and boat/stick
        Physics2D.IgnoreLayerCollision(8, 12);
        Physics2D.IgnoreLayerCollision(9, 12);

        //Ignore collision between border and net/stick
        Physics2D.IgnoreLayerCollision(9, 13);
        Physics2D.IgnoreLayerCollision(10, 13);
    }

    //On update, checks if the game is over and if timeScale is different to 0.
    //On that case, checks for pressed keys.
    void Update()
    {
        if (!GameController.instance.gameOver && Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(new Vector2(-force, 0.0f));
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(new Vector2(force, 0.0f));
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1.0f, 1.0f, 1.0f));
            }
        }
    }
}
