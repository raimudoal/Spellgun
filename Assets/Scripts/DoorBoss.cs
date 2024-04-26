using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBoss : MonoBehaviour
{
    public ElementActivate fire;
    public ElementActivate water;
    public ElementActivate electric;
    public ElementActivate stone;
    bool once = false;
    private PlayerReset player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("resetter").GetComponent<PlayerReset>();
    }
    // Update is called once per frame
    void Update()
    {
        if (fire.ReturnStatus(fire) && !once)
        {
            if (water.ReturnStatus(water))
            {
                if (electric.ReturnStatus(electric))
                {
                    if (stone.ReturnStatus(stone))
                    {
                        Debug.Log("OPEN");
                        once = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (once && player.HasKeys())
            {
                SceneManager.LoadScene(4);
            }
        }
    }
}
