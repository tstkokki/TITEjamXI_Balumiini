﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player_Controller_Script : MonoBehaviour
{
    private CharacterController sugarCtrl;
    public UI_Master_Script ui_master;
    [Header("Movement")]
    private float xMove = 0f;
    public float moveSpeed = 8f;
    public float inputAcceleration = 0f;
    public float inputThreshold = 0.2f;
    private Vector3 dir = Vector3.zero;
    public bool MoveDemo = false;

    [Header("Jumping and falling")]
    public float jumpHeight = 4f;
    private float yMove = 0f;
    public float groundDist = 0.2f;
    private float gravity = -18.81f;
    public Transform groundCheck; //object at the feet of character
    private bool isGrounded;
    public LayerMask groundMask;
    public int tornadoPoints = 0;
    public float tornadoCD = 1.0f;

    [Header("Effects")]
    public ParticleSystem[] PlayerEffects;
    private List<ParticleSystem> effectsPool = new List<ParticleSystem>();
    public AudioSource playerAudio;
    public AudioClip []playerSounds;
    /*Sound listing
     0 = Jump
     1 = water splash
     2 = walking
     3 = running
     4 = yummy
     5 = coin sugar pickup
     6 = tornado
         */
         /*Effect listing
          * 0 = splash
          * 1 = land
          * 2 = pow
          * 3 = tornado
         */

    // Start is called before the first frame update
    void Start()
    {
        sugarCtrl = GetComponent<CharacterController>();
        playerAudio = GetComponent<AudioSource>();

        for(int i = 0; i < PlayerEffects.Length; i++)
        {
            ParticleSystem _eff = Instantiate(PlayerEffects[i], transform);
            effectsPool.Add(_eff);
            _eff.gameObject.SetActive(false);
        }
        ui_master = FindObjectOfType<UI_Master_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (!isGrounded)
        {
            yMove += gravity * Time.deltaTime;
            if (Input.GetButtonDown("Fire3") && tornadoPoints >= 20)
            {
                yMove = Mathf.Sqrt(jumpHeight * -2f * gravity);
                playerAudio.PlayOneShot(playerSounds[6]);
                PlayEffectPool(3);
                tornadoPoints = ui_master.IncreaseSugarScore(-20);
            }
        }
        else
        {
            if (dir.y < 0)
            {
                yMove = -2f;
                PlayEffectPool(1);
            }
            if(Input.GetButtonDown("Jump"))
            {
                yMove = Mathf.Sqrt(jumpHeight * -2f * gravity);
                playerAudio.PlayOneShot(playerSounds[0]);
                
            }
        }


        if (MoveDemo)
        {
            if (Input.GetAxis("Horizontal") > inputThreshold || Input.GetAxis("Horizontal") < -inputThreshold)
            {
                xMove = Input.GetAxis("Horizontal");
            }
            else xMove = 0;
            inputAcceleration = Mathf.Clamp(Mathf.Sqrt(Mathf.Pow(xMove, 2f)), 0, 1);
        }
        else
        {
            xMove = Input.GetAxis("Horizontal") * moveSpeed;
        }
        dir.y = yMove;
        dir.x = xMove;
        //transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
        sugarCtrl.Move(dir * Time.deltaTime);
        if (isGrounded && (sugarCtrl.velocity.x > 0 || sugarCtrl.velocity.x < 0) && !playerAudio.isPlaying && sugarCtrl.velocity.y > -1f && sugarCtrl.velocity.y < 1f)
        {
            if(sugarCtrl.velocity.x > 5 || sugarCtrl.velocity.x < -5)
            {
                playerAudio.PlayOneShot(playerSounds[3]);
            }
            else playerAudio.PlayOneShot(playerSounds[2]);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 4)
        {
            PlayEffectPool(0);
            playerAudio.PlayOneShot(playerSounds[1], 0.6f);
        }
        if (other.gameObject.CompareTag("Collectable"))
        {
            Debug.Log("Collided with sugar");
            playerAudio.PlayOneShot(playerSounds[4]);
            playerAudio.PlayOneShot(playerSounds[5], 0.6f);
            other.gameObject.transform.parent.gameObject.SetActive(false);
            tornadoPoints = ui_master.IncreaseSugarScore(10);
            PlayEffectPool(2);
        }
    }

    void PlayEffectPool(int _index)
    {
        effectsPool[_index].gameObject.SetActive(true);
        effectsPool[_index].Play();
    }
}
