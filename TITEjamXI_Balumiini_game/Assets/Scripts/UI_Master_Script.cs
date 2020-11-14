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
    public TextMeshProUGUI heightText;
    private Transform Player;

    public Platform_Master_Script platMaster;
    public Wall_Master_Script wallMaster;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Player_Controller_Script>().gameObject.transform;
        platMaster = FindObjectOfType<Platform_Master_Script>();
        wallMaster = FindObjectOfType<Wall_Master_Script>();
    }

    private void LateUpdate()
    {
        CalcPlayerHeight();
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
