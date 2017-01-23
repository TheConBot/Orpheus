using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SpawnEnemy : MonoBehaviour {

    public enum Enemy
    {
        Lost_Soul,
        Damned_Soul,
        Fury,
        Hades,
        Cerberus
    }
    public Enemy whatEnemy;
    public GameObject[] Lost_Soul_Pattern;
    public GameObject[] Damned_Soul_Pattern;
    public GameObject[] Fury_Pattern;
    public GameObject[] Hades_Pattern;
    public GameObject[] Cerberus_Pattern;
    public float roundTime = 10;
    public Text timer;
    public HitDetect hitDetect;
    public AudioSource victoryTheme;
    public AudioSource battleTheme;
    public AudioSource gameOverTheme;
    public AudioClip hadesTheme;
    public CanvasGroup fade;
    public WaveMaker waveMaker;
    public GameObject gameOver_panel;

    private bool enableTimer;
    private bool gameOver;

    private void Start() {
        fade.alpha = 1;
        whatEnemy = LevelManager.m.enemyToLoad;
        GameObject[] toSpawn = null;
        switch (whatEnemy)
        {
            case Enemy.Lost_Soul:
                toSpawn = Lost_Soul_Pattern;
                break;
            case Enemy.Hades:
                toSpawn = Hades_Pattern;
                roundTime = 56.66f;
                battleTheme.clip = hadesTheme;
                battleTheme.Play();
                break;
            case Enemy.Fury:
                toSpawn = Fury_Pattern;
                break;
            case Enemy.Damned_Soul:
                toSpawn = Damned_Soul_Pattern;
                break;
            case Enemy.Cerberus:
                toSpawn = Cerberus_Pattern;
                roundTime = 27.50f;
                break;
        }
        timer.text = roundTime.ToString("F");
        StartCoroutine(Spawn(toSpawn));
    }

    private void Update()
    {
        if (enableTimer)
        {
            roundTime -= Time.deltaTime;
            roundTime = Mathf.Clamp(roundTime, 0.00f, Mathf.Infinity);
            //timer.text = roundTime.ToString();
            timer.text = roundTime.ToString("F");

            if (roundTime <= 0)
            {
                StartCoroutine(BattleWin());
                enableTimer = false;
            }
        }

        if(gameOver && Input.GetKeyDown(KeyCode.Space)){
            LevelManager.m.ReloadLevel();
        }
    }

    private IEnumerator BattleWin()
    {
        hitDetect.enabled = false;
        battleTheme.Stop();
        victoryTheme.Play();
        timer.text = "Enemy Charmed!";
        while (victoryTheme.isPlaying)
        {
            yield return null;
        }
        while (fade.alpha != 1)
        {
            fade.alpha += 0.05f;
            yield return null;
        }
        LevelManager.m.StartCoroutine(LevelManager.m.LoadOverworld());
        yield return null;
    }

    public IEnumerator GameOver()
    {
        waveMaker.enabled = false;
        enableTimer = false;
        timer.text = "";
        battleTheme.Stop();
        gameOverTheme.Play();
        yield return new WaitForSeconds(2f);
        while (fade.alpha != 1)
        {
            fade.alpha += 0.05f;
            yield return null;
        }
        gameOver_panel.SetActive(true);
        gameOver = true;
    }

    private IEnumerator Spawn(GameObject[] objects)
    {
        while(fade.alpha != 0)
        {
            fade.alpha -= 0.05f;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        foreach (GameObject thing in objects)
        {
            GameObject i = Instantiate(thing);
            i.transform.SetParent(transform);
        }
        enableTimer = true;
        yield return null;
    }
}
