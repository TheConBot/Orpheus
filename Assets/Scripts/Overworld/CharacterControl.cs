using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public float moveSpeed;
    public AudioSource overWorldTheme;
    public AudioSource battleEnterTheme;
    public AudioSource hadesEnterSound;
    public AudioSource mainMenuMusic;

    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        int xInput = 0;
        int yInput = 0;
        anim.enabled = true;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            xInput = 1;
            anim.SetBool("BackWalk", false);
            anim.SetBool("RightWalk", true);
            anim.SetBool("LeftWalk", false);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            xInput = -1;
            anim.SetBool("BackWalk", false);
            anim.SetBool("RightWalk", false);
            anim.SetBool("LeftWalk", true);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            yInput = 1;
            anim.SetBool("BackWalk", true);
            anim.SetBool("RightWalk", false);
            anim.SetBool("LeftWalk", false);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            yInput = -1;
            anim.SetBool("BackWalk", false);
            anim.SetBool("RightWalk", false);
            anim.SetBool("LeftWalk", false);
        }
        else
        {
            anim.enabled = false;
        }
        rb.MovePosition(new Vector2(rb.position.x + (xInput * Time.deltaTime * moveSpeed), rb.position.y + (yInput * Time.deltaTime * moveSpeed)));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lost Soul" || other.tag == "Damned Soul" || other.tag == "Hades" || other.tag == "Cerberus" || other.tag == "Fury")
        {
            other.gameObject.layer = 8;
            switch (other.tag)
            {
                case "Lost Soul":
                    LevelManager.m.enemyToLoad = SpawnEnemy.Enemy.Lost_Soul;
                    break;
                case "Damned Soul":
                    LevelManager.m.enemyToLoad = SpawnEnemy.Enemy.Damned_Soul;
                    break;
                case "Cerberus":
                    LevelManager.m.enemyToLoad = SpawnEnemy.Enemy.Cerberus;
                    break;
                case "Fury":
                    LevelManager.m.enemyToLoad = SpawnEnemy.Enemy.Fury;
                    break;
                case "Hades":
                    LevelManager.m.enemyToLoad = SpawnEnemy.Enemy.Hades;
                    break;
            }
            overWorldTheme.Stop();
            if (other.tag == "Hades")
            {
                hadesEnterSound.Play();
            }
            else {
                battleEnterTheme.Play();
            }
            anim.enabled = false;
            LevelManager.m.StartCoroutine(LevelManager.m.LoadBattle(gameObject, other.gameObject));
            enabled = false;
        }
        else if(other.tag == "Gate")
        {
            LevelManager.m.StartCoroutine(LevelManager.m.LoadNextLevel());
            enabled = false;
        }
        else if(other.tag == "Ayla")
        {
            overWorldTheme.Stop();
            mainMenuMusic.Play();
            LevelManager.m.StartCoroutine(LevelManager.m.EndGame());
            enabled = false;
        }
    }
}
