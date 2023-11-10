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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Singleplayer()
    {
        SceneManager.LoadScene(sceneBuildIndex:1);
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }
}
