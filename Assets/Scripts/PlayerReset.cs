using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerReset : MonoBehaviour
{
    PlayerMovement player;
    CheckpointManager checkpointManager;
    public Vector3 lastCheckPointSaved;
    bool active = false;
    public bool key1;
    public bool key2;

    private Image fireUI, waterUI, electricUI, stoneUI;

    public bool fire;
    public bool water;
    public bool electric = true;
    public bool stone;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("resetter");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public bool HasKeys()
    {
        if (key1 && key2)
        {
            return true;
        }
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("FinalBossRoom"))
        {
            lastCheckPointSaved = new Vector3(47.5f, -16, 0);
        }
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            fire = false;
            water = false;
            stone = false;
            key1 = false;
            key2 = false;
        }
        player = FindObjectOfType<PlayerMovement>();
        checkpointManager = FindObjectOfType<CheckpointManager>();
        if (player)
        {
            if (player.health == 0 && !active)
            {
                StartCoroutine(StopAndTP());
            }

        }
        fireUI = GameObject.FindGameObjectWithTag("PouchFireUI").GetComponent<Image>();
        waterUI = GameObject.FindGameObjectWithTag("PouchWaterUI").GetComponent<Image>();
        electricUI = GameObject.FindGameObjectWithTag("PouchElectricUI").GetComponent<Image>();
        stoneUI = GameObject.FindGameObjectWithTag("PouchStoneUI").GetComponent<Image>();

        if (fireUI && fire)
        {
            fireUI.color = Color.white;
        }
        else
        {
            fireUI.color = new Color(1, 1, 1, 0);
        }
        if (waterUI && water)
        {
            waterUI.color = Color.white;
        }
        else
        {
            waterUI.color = new Color(1, 1, 1, 0);
        }
        if (electricUI && electric)
        {
            electricUI.color = Color.white;
        }
        if (stoneUI && stone)
        {
            stoneUI.color = Color.white;
        }
        else
        {
            stoneUI.color = new Color(1, 1, 1, 0);
        }
    }

    IEnumerator StopAndTP()
    {
        active = true;
        yield return new WaitForSeconds(5.6f);
        player.transform.position = lastCheckPointSaved;
        active = false;
    }
}
