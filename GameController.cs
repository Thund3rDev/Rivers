using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.LWRP;

//Class GameController, that controls all game behaviours.
public class GameController : MonoBehaviour
{
    //Singleton.
    public static GameController instance;

    //Variables.
    private int fishes = 0;
    public TextMeshProUGUI fishCounterText;

    public float velocity = 1;
    public ParticleSystem ps;

    public SpriteRenderer water;
    public int actualColor;

    private float timeCounter;

    public bool gameOver = true;
    public GameObject ingameUI;
    public GameObject endGame;
    public TextMeshProUGUI[] finalTexts = new TextMeshProUGUI[4];

    //Tutorial variables.
    public GameObject[] popUps;
    private int popUpIndex;
    public GameObject pointLight;
    public GameObject globalLight;
    public GameObject boat;
    public GameObject net;
    private float waitingTime = 2f;
    private bool[] conditions = new bool[4];
    public GameObject generator;

    //On start, instantiate itself, set the timeScale to 1,
    //get the time on timeCounter and set the water color to initial.
    //Also set popUpIndex to 0 and the pointLight position (for tutorial).
    void Start()
    {
        instance = this;
        Time.timeScale = 1;
        timeCounter = Time.time;
        water.color = new Color32(0, 202, 255, 168);

        popUpIndex = 0;
        pointLight.transform.position = new Vector3(0, -3.37f, 0);
    }

    //Update is called once per frame.
    void Update()
    {
        //Tutorial
        if (!GlobalVars.instance.tutorialDone)
        {
            switch (popUpIndex)
            {
                //Depending on the case, check the condition and wait player input.
                case 0:
                    if (!conditions[0])
                    {
                        Time.timeScale = 0;
                        popUps[0].SetActive(true);
                        pointLight.SetActive(true);
                        globalLight.GetComponent<Light2D>().intensity = 0.4f;
                    }
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                    {
                        conditions[0] = true;
                        popUps[0].SetActive(false);
                        pointLight.SetActive(false);
                        globalLight.GetComponent<Light2D>().intensity = 1f;
                        Time.timeScale = 1;
                    }
                    else
                    {
                        if (waitingTime <= 0)
                        {
                            popUpIndex++;
                            waitingTime = 2f;
                        }
                        else
                            waitingTime -= Time.deltaTime;
                    }
                    break;
                case 1:
                    if (!conditions[1])
                    {
                        Time.timeScale = 0;
                        popUps[1].SetActive(true);
                        pointLight.transform.position = net.transform.position;
                        pointLight.GetComponent<Light2D>().pointLightOuterRadius = 1.0f;
                        pointLight.SetActive(true);
                        globalLight.GetComponent<Light2D>().intensity = 0.4f;
                    }
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        Time.timeScale = 1;
                        conditions[1] = true;
                        popUps[1].SetActive(false);
                        pointLight.SetActive(false);
                        globalLight.GetComponent<Light2D>().intensity = 1f;
                    }
                    else
                    {
                        if (waitingTime <= 0)
                        {
                            popUpIndex++;
                            waitingTime = 2f;
                        }
                        else
                            waitingTime -= Time.deltaTime;
                    }
                    break;
                case 2:
                    if (!conditions[2])
                    {
                        Time.timeScale = 0;
                        popUps[2].SetActive(true);

                        ItemsController[] items = generator.GetComponentsInChildren<ItemsController>();
                        ItemsController rockTutorial = items[0];

                        pointLight.transform.position = rockTutorial.transform.position;
                        pointLight.SetActive(true);
                        globalLight.GetComponent<Light2D>().intensity = 0.4f;
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Time.timeScale = 1;
                        conditions[2] = true;
                        popUps[2].SetActive(false);
                        pointLight.SetActive(false);
                        globalLight.GetComponent<Light2D>().intensity = 1f;
                    }
                    else
                    {
                        if (waitingTime <= 0)
                        {
                            popUpIndex++;
                            waitingTime = 2f;
                        }
                        else
                            waitingTime -= Time.deltaTime;
                    }
                    break;
                case 3:
                    if (!conditions[3])
                    {
                        Time.timeScale = 0;
                        popUps[3].SetActive(true);

                        ItemsController[] items = generator.GetComponentsInChildren<ItemsController>();
                        ItemsController fishTutorial = items[1];

                        pointLight.transform.position = fishTutorial.transform.position;
                        pointLight.GetComponent<Light2D>().intensity = 2f;
                        pointLight.SetActive(true);
                        globalLight.GetComponent<Light2D>().intensity = 0.4f;
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Time.timeScale = 1;
                        conditions[3] = true;
                        popUps[3].SetActive(false);
                        pointLight.SetActive(false);
                        globalLight.GetComponent<Light2D>().intensity = 1f;
                    }
                    else
                    {
                        if (waitingTime <= 0)
                        {
                            popUpIndex++;
                            waitingTime = 2f;
                        }
                        else
                            waitingTime -= Time.deltaTime;
                    }
                    break;
                case 4:
                    //End of tutorial. Get the initial time, set gameOver to false and tutorialDone to true.
                    timeCounter = Time.time;
                    gameOver = false;
                    GlobalVars.instance.tutorialDone = true;
                    break;
            }
        } else
        {
            //Increment velocity.
            velocity += 0.036f * Time.deltaTime;
            var psMain = ps.main;
            psMain.simulationSpeed = velocity;
        }

        //Water changes based on velocity value (~30s, ~1m, ~1m30s, ~2m).
        if (!gameOver)
        {
            if (velocity > 2.08 && actualColor == 0)
            {
                if (water.color.g > 0.55f)
                    water.color -= new Color(0, 0.002f, 0, 0);
                else
                    actualColor++;
            }

            if (velocity > 3.16 && actualColor == 1)
            {
                if (water.color.g > 0.4f)
                    water.color -= new Color(0, 0.002f, 0, 0);
                else
                    actualColor++;
            }

            if (velocity > 4.24 && actualColor == 2)
            {
                if (water.color.b > 0.7f)
                    water.color -= new Color(0, 0, 0.002f, 0);
                else if (water.color.g > 0.3f)
                    water.color -= new Color(0, 0.002f, 0, 0);
                else
                    actualColor++;
            }

            if (velocity > 5.32 && actualColor == 3)
            {
                if (water.color.b > 0.2f)
                    water.color -= new Color(0, 0, 0.002f, 0);
                else if (water.color.g > 0.1f)
                    water.color -= new Color(0, 0.002f, 0, 0);
                else
                    actualColor++;
            }
        }
    }

    //Function TakeFish, to update the score.
    public void TakeFish()
    {
        fishes++;
        fishCounterText.text = "" + fishes;
        AudioManager.instance.ManageAudio("takeFish", "sound", "play");
    }

    //Function FinishGame, called when game ends.
    public void FinishGame(string crash)
    {
        //Stop the game.
        Time.timeScale = 0;
        gameOver = true;
        AudioManager.instance.ManageAudio("boatCrack", "sound", "play");

        //Save the time the player has been playing a match.
        timeCounter = Time.time - timeCounter;
        int seconds = (int)(timeCounter % 60);
        int minutes = (int)((timeCounter / 60) % 60);

        string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);

        //Update the best score.
        if (fishes > GlobalVars.instance.bestScore)
            GlobalVars.instance.bestScore = fishes;

        //Update the texts in final screen.
        finalTexts[0].text = "You broke the " + crash + "!";
        finalTexts[1].text = "Fishes: " + fishes + ". (Best: " + GlobalVars.instance.bestScore + ").";
        finalTexts[2].text = "Time on the river: " + timerString + ".";

        if (fishes == 0)
            finalTexts[3].text = "It's your first time?";

        if (fishes > 0)
            finalTexts[3].text = "A short catch, better luck next time.";

        if (fishes > 20)
            finalTexts[3].text = "Not bad, but you can improve.";

        if (fishes > 50)
            finalTexts[3].text = "Good job, that was a nice catch.";

        if (fishes > 100)
            finalTexts[3].text = "WOW! That was great!";

        if (fishes > 300)
            finalTexts[3].text = "How the f*** did you catch that?";

        //Activate the endGame UI and deactivate in-game UI.
        ingameUI.SetActive(false);
        endGame.SetActive(true);
    }
}
