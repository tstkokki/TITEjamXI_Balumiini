using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI_Master_Script : MonoBehaviour
{
    public float curHeight = 0f;
    private float step = 0f;
    private float wallStep = 0f;
    private int platScale = 1;
    public float playTime = 0f;
    public int sugarScore = 0;
    public TextMeshProUGUI heightText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI diffText;
    public TextMeshProUGUI scoreText;
    private Transform Player;

    [Header("Masters")]
    public Platform_Master_Script platMaster;
    public Wall_Master_Script wallMaster;
    public EvilWaterScript evilWater;
    public Sugar_Master_Script sugarMaster;
    public AudioMixer sugarMixer;
    public AudioMixerSnapshot lowPass;
    public AudioMixerSnapshot normalAudio;

    [Header("Game Over UI")]
    public GameObject GameOverPanel;
    public TextMeshProUGUI FinalScoreTexts;
    public Button RetryButton;
    public Button ReturnToMenuButton;

    public enum WaterState
    {
        Low = 0,
        Intermediate = 1,
        Hard = 2,
        GettingMoist = 3,
        Splashy = 4,
        BathroomLeakage = 5,
        Flood = 6,
        Tsunami = 7
    }
    public WaterState curTide = WaterState.Low;
    // Start is called before the first frame update
    void Start()
    {
        normalAudio.TransitionTo(0.2f);
        Player = FindObjectOfType<Player_Controller_Script>().gameObject.transform;
        platMaster = FindObjectOfType<Platform_Master_Script>();
        wallMaster = FindObjectOfType<Wall_Master_Script>();
        evilWater = FindObjectOfType<EvilWaterScript>();
        sugarMaster = FindObjectOfType<Sugar_Master_Script>();
        diffText.text = curTide.ToString();
        scoreText.text = "0 sucrose";
    }

    private void FixedUpdate()
    {
        CalcTime();
    }

    private void LateUpdate()
    {
        CalcPlayerHeight();
    }

    void CalcTime()
    {
        playTime += Time.fixedDeltaTime;
        timeText.text = playTime.ToString("0.00") + " s";
        if(curTide == WaterState.Low && playTime >= 10f)
        {
            DifficultyUpdate();
        }
        if (curTide == WaterState.Intermediate && playTime >= 20f)
        {
            DifficultyUpdate();
        }
        if (curTide == WaterState.Hard && playTime >= 35f)
        {
            DifficultyUpdate();
        }
        if (curTide == WaterState.GettingMoist && playTime >= 50f)
        {
            DifficultyUpdate();
        }
        if (curTide == WaterState.Splashy && playTime >= 110f)
        {
            DifficultyUpdate();
        }
        if (curTide == WaterState.BathroomLeakage && playTime >= 130f)
        {
            DifficultyUpdate();
        }
        if (curTide == WaterState.Flood && playTime >= 180f)
        {
            DifficultyUpdate();
            lowPass.TransitionTo(1f);
        }
    }

    void DifficultyUpdate()
    {
        curTide = (WaterState)curTide + 1;
        evilWater.IncreaseTide((float)curTide);
        string _diff = curTide.ToString();
        for(int i = 0; i < _diff.Length; i++)
        {
            if(char.IsUpper(_diff[i]) && i > 0)
            {
                _diff = _diff.Substring(0, i) + " " + _diff.Substring(i);
                break;
            }
        }
        diffText.text = _diff;
        platScale = (int)curTide;
        platMaster.SetPlatScale(platScale);
    }

    void CalcPlayerHeight()
    {
        curHeight = Player.position.y;
        heightText.text = curHeight.ToString("0.0") + " cm";
        StepCheck();
    }

    void StepCheck()
    {
        if( curHeight >= step + 4f)
        {
            step = curHeight;
            platMaster.SpawnNextPlatform(Player.position.y);
        }
        if(curHeight >= wallStep + 20f)
        {
            wallStep = curHeight;
            wallMaster.SpawnNextPlatform();
            sugarMaster.SpawnNextPlatform(Player.position.y);
        }
    }

    public int IncreaseSugarScore(int _score)
    {
        sugarScore += _score;
        scoreText.text = sugarScore.ToString() + " sucrose";
        return sugarScore;
    }

    #region Game Over menu stuff
    public void CalculateScore()
    {
        //show game over paneö
        GameOverPanel.SetActive(true);

        //calculate final score
        //sugar points
        FinalScoreTexts.text = sugarScore.ToString();
        //time bonus
        FinalScoreTexts.text += "\n" + playTime.ToString("0.00");
        //height bonus
        FinalScoreTexts.text += "\n" + curHeight.ToString("0.00");
        //total
        FinalScoreTexts.text += "\n\n" + ((float)sugarScore + playTime + curHeight).ToString("0");

        // create listeners for buttons
        RetryButton.onClick.AddListener(ClickReloadLevel);
        ReturnToMenuButton.onClick.AddListener(ReturnToMenu);
    }

    void ClickReloadLevel()
    {
        //reload play scene
        SceneManager.LoadScene(1);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

}
