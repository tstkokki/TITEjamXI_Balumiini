using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Master_Script : MonoBehaviour
{
    public float curHeight = 0f;
    private float step = 0f;
    private float wallStep = 0f;
    public float playTime = 0f;
    public TextMeshProUGUI heightText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI diffText;
    private Transform Player;

    [Header("Masters")]
    public Platform_Master_Script platMaster;
    public Wall_Master_Script wallMaster;
    public EvilWaterScript evilWater;



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
        Player = FindObjectOfType<Player_Controller_Script>().gameObject.transform;
        platMaster = FindObjectOfType<Platform_Master_Script>();
        wallMaster = FindObjectOfType<Wall_Master_Script>();
        evilWater = FindObjectOfType<EvilWaterScript>();
        diffText.text = curTide.ToString();
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
        if (curTide == WaterState.Flood && playTime >= 160f)
        {
            DifficultyUpdate();
        }
        if (curTide == WaterState.Tsunami && playTime >= 200f)
        {
            DifficultyUpdate();
        }
    }

    void DifficultyUpdate()
    {
        curTide = (WaterState)curTide + 1;
        evilWater.IncreaseTide((float)curTide);
        string _diff = curTide.ToString();
        for(int i = 0; i < _diff.Length; i++)
        {
            Debug.Log("Iterating " + _diff);
            if(char.IsUpper(_diff[i]) && i > 0)
            {
                _diff = _diff.Substring(0, i) + " " + _diff.Substring(i);
                break;
            }
        }
        diffText.text = _diff;
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
        }
    }
}
