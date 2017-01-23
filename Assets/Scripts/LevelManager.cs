using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager m { get; private set; }
    public SpawnEnemy.Enemy enemyToLoad;
    [HideInInspector]
    public List<GameObject> savedObjects = new List<GameObject>();
    [HideInInspector]
    public bool loadingLevel;

    private int levelToLoad;
    private GameObject mainCharacter;
    private GameObject enemy;

    void Awake()
    {
        if (m != null && m != this)
        {
            // Destroy if another Gamemanager already exists
            Destroy(gameObject);
        }
        else {

            // Here we save our singleton instance
            m = this;
            // Furthermore we make sure that we don't destroy between scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    public IEnumerator EndGame()
    {
        CanvasGroup fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<CanvasGroup>();
        while (fade.alpha != 1)
        {
            fade.alpha += 0.05f;
            yield return null;
        }
        GameObject.FindGameObjectWithTag("EndGame").GetComponent<CanvasGroup>().alpha = 1;
        yield return new WaitForSeconds(10);
        Application.Quit();
        yield return null;
    }

    public IEnumerator LoadNextLevel()
    {
        CanvasGroup fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<CanvasGroup>();
        while(fade.alpha != 1)
        {
            fade.alpha += 0.05f;
            yield return null;
        }
        foreach(GameObject i in savedObjects)
        {
            Destroy(i);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }

    public IEnumerator LoadOverworld()
    {
        loadingLevel = true;
        SceneManager.LoadScene(levelToLoad);
        foreach(GameObject i in savedObjects)
        {
            if (i != enemy)
            {
                i.SetActive(true);
            }
        }
        savedObjects.Remove(enemy);
        Destroy(enemy);
        mainCharacter.GetComponent<CharacterControl>().enabled = true;
        yield return null;
    }

    public IEnumerator LoadBattle(GameObject mc, GameObject e)
    {
        mainCharacter = mc;
        enemy = e;
        int oldMask = Camera.main.cullingMask;
        Camera.main.cullingMask = (1 << LayerMask.NameToLayer("Battle"));
        SpriteRenderer mcsr = mainCharacter.GetComponent<SpriteRenderer>();
        SpriteRenderer esr = enemy.GetComponent<SpriteRenderer>();
        for (int i = 0; i < 3; i++)
        {
            mcsr.enabled = false;
            esr.enabled = false;
            yield return new WaitForSeconds(0.15f);
            mcsr.enabled = true;
            esr.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }
        foreach (GameObject i in savedObjects)
        {
            i.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        levelToLoad = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Battle");
        yield return null;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
