using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public CanvasGroup fade;
    public CanvasGroup[] storyBits;

    private int index;
    private bool startStory;

	public void Play()
    {
        StartCoroutine(PlayStory());
    }

    private void Update()
    {
        if (startStory && Input.GetKeyDown(KeyCode.Space))
        {
            index++;
            if(index > storyBits.Length - 1)
            {
                StartCoroutine(StartGame());
            }
            else
            {
                storyBits[index - 1].alpha = 0;
                storyBits[index].alpha = 1;
            }
        }
    }

    private IEnumerator PlayStory()
    {
        while (fade.alpha != 1)
        {
            fade.alpha += 0.05f;
            yield return null;
        }
        startStory = true;
        storyBits[index].alpha = 1;
        yield return null;
    }

    private IEnumerator StartGame()
    {
        storyBits[index - 1].alpha = 0;
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
