using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameHasEnded = false;
    public float restartdelay = 3f;
    public Canvas loseScreen;
    public GameObject levelCompletedUI;
    
    public GameObject player;
    public Canvas missionBriefing;
    public float missionBriefingTime = 10f;
    public GameObject[] briefingtexts;
    public GameObject briefingtextsGameObject;
    public float timesincestart = 0;
    private bool countdownstarted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        foreach (var text in briefingtexts)
        {
           text.SetActive(false); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        timesincestart = timesincestart + Time.deltaTime;

        if (timesincestart < 5)
        {
            if (timesincestart > 0.99f && timesincestart < 1.01f)
            {
                briefingtexts[0].SetActive(true);
            }
            if (timesincestart > 1.99f && timesincestart < 2.01f)
            {
                briefingtexts[1].SetActive(true);
            }
            if (timesincestart > 2.99f && timesincestart < 3.01f)
            {
                briefingtexts[2].SetActive(true);
            }
            if (timesincestart > 3.99f && timesincestart < 4.01f)
            {
                briefingtexts[3].SetActive(true);
                countdownstarted = true;
                StartCoroutine(closeMissionBriefing(missionBriefingTime));
            }
        }
            

            if (countdownstarted)
            {
                if (missionBriefingTime > 0)
                {
                    missionBriefingTime -= Time.deltaTime;
                    DisplayTime(missionBriefingTime);
                }
                else
                {
                    //Debug.Log("Have fun");
                    missionBriefingTime = 0;
                    countdownstarted = false;
                    missionBriefing.gameObject.SetActive(false);
                }
            }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        briefingtexts[3].GetComponent<TextMeshProUGUI>().text = "Mission starts in: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void WinLevel()
    {
        levelCompletedUI.SetActive(true);
        Invoke("SwitchBackToMainMenu", 5f);
    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            loseScreen.gameObject.SetActive(true);
            Invoke("Restart", restartdelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SwitchBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator closeMissionBriefing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        missionBriefing.gameObject.SetActive(false);
    }
    
    //Background image source: https://wallpaperaccess.com/full/11046.jpg
    //Timer code from https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
}
