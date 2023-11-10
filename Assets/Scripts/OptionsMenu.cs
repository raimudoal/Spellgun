using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
