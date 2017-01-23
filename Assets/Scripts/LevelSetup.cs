using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour {

    public List<GameObject> savedObjects = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(FadeIn(GameObject.FindGameObjectWithTag("Fade").GetComponent<CanvasGroup>()));
        if (!LevelManager.m.loadingLevel)
        {
            LevelManager.m.savedObjects = savedObjects;
            foreach (GameObject i in savedObjects)
            {
                DontDestroyOnLoad(i);
            }
        }
        else
        {
            for(int i = 0; i < savedObjects.Count; i++)
            {
                Destroy(savedObjects[i]);
            }
            LevelManager.m.loadingLevel = false;
        }
    }

    private IEnumerator FadeIn(CanvasGroup fade)
    {
        fade.alpha = 1;
        while(fade.alpha != 0)
        {
            fade.alpha -= 0.05f;
            yield return null;
        }
        yield return null;
    }
}
