using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance Checkpoint dans la scène");
            return;
        }

        instance = this;
    }

    public void spawnTo(int check_number, GameObject player)
    {
        var playerPosition = player.transform.position;
        switch (check_number)
        {
            case 0:
                playerPosition.x = -0.534f;
                playerPosition.y = 0.502f;
                player.transform.position = playerPosition;
                break;
            case 1:
                playerPosition.x = 9.22f;
                playerPosition.y = 60.28f;
                player.transform.position = playerPosition;
                break;
            case 2:
                playerPosition.x = 18.499f;
                playerPosition.y = 125.258f;
                player.transform.position = playerPosition;
                break;
        }

        player.GetComponent<PlayerHealth>().health += 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
