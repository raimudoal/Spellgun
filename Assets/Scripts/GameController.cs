using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public float deathPos;
    Scene currentScene;
    CheckpointManager checkpointManager;
    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = GetComponent<CheckpointManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
