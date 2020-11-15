using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneController : MonoBehaviour
{
    public Animator doorLeft;
    public Animator doorRight;
    public Button creditsButton;
    public GameObject creditsPanel;
    bool isTransitioning;

    public void ToggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTransitioning)
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        isTransitioning = true;
        doorLeft.Play("DoorLeft");
        doorRight.Play("DoorRight");
        StartCoroutine("SwapScene");
        //SceneManager.LoadScene("Harton_scene");
    }

    IEnumerator SwapScene()
    {
        yield return new WaitForSeconds(2.1f);
        SceneManager.LoadScene(1);
    }
}
