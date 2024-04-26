using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(int scene)
    {
        StartCoroutine(MakeTransition(scene));
    }

    IEnumerator MakeTransition(int scene)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneBuildIndex: scene);
    }
}
