using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float deathPos;
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
        if (player.transform.position.y < deathPos)
        {
            GoToLastCheckPoint();
        }
    }

    private void GoToLastCheckPoint()
    {
        player.transform.position = checkpointManager.GetLastCheckPointPosition();
    }
}
