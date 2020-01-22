using UnityEngine;
using UnityEngine.SceneManagement;

//Class Generation, to generate items on menu and game.
public class Generation : MonoBehaviour
{
    //Variables.
    public GameObject fish;
    public GameObject[] obstacles = new GameObject[6];
    public GameObject background;

    //On Start, call the functions to generate a fish and an item.
    void Start()
    {
        Invoke("GenerateObstacle", 1);
        Invoke("GenerateFish", 2);
    }

    //Function GenerateFish, to instantiate a fish on the menu or game.
    private void GenerateFish()
    {
        //Float rTime, to control the random time a fish should be generated.
        float rTime;

        //If scene is menu, parameters are other than at game.
        if (SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            float rPosX = Random.Range(7f, 8f);
            float rPosY = Random.Range(1.5f, 4f);
            float rRot = Random.Range(-75f, -85f);
            GameObject thisFish = Instantiate(fish, new Vector3(rPosX, rPosY, 2.0f), Quaternion.Euler(0.0f, 0.0f, rRot), background.transform);
            thisFish.transform.localScale = new Vector3(0.34f, 0.34f, 1.0f);
            rTime = Random.Range(2.0f, 4.0f);
        }
        else
        {
            float rPos = Random.Range(-5.8f, 5.8f);
            Instantiate(fish, new Vector3(rPos, 6f, 2.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), gameObject.transform);

            float velMinTime = 1f - ((GameController.instance.velocity - 1) / 2);
            if (velMinTime < 0)
                velMinTime = 0;

            rTime = Random.Range(velMinTime, velMinTime + 1);
        }

        //Call itself.
        Invoke("GenerateFish", rTime);
    }

    //Function GenerateObstacle, to instantiate an obstacle in the game.
    private void GenerateObstacle()
    {
        //If scene is not menu (what means scene is game, due to we've only two scenes)...
        if (!SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            int obstacleLimit = 5;
            switch (GameController.instance.actualColor)
            {
                case 0:
                    obstacleLimit = 5;
                    break;
                case 1:
                    obstacleLimit = 6;
                    break;
            }

            int rRock = (int)Random.Range(0, obstacleLimit);
            float rPos = Random.Range(-5.8f, 5.8f);
            Instantiate(obstacles[rRock], new Vector3(rPos, 6f, -2.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), gameObject.transform);

            float velMinTime = 2f - (GameController.instance.velocity - 1);
            if (velMinTime < 0)
                velMinTime = 0;

            //Float rTime, to control the random time an obstacle should be generated.
            float rTime = Random.Range(velMinTime, velMinTime + 1);

            //Call itself.
            Invoke("GenerateObstacle", rTime);
        }
    }
}
