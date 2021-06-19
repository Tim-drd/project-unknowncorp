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
                playerPosition.x = 7f;//-0.534f; //plage;
                playerPosition.y = 153f;//0.502f;
                player.transform.position = playerPosition;
                break;
            case 1:
                playerPosition.x = 9.22f; //arbre mojo;
                playerPosition.y = 60.28f;
                player.transform.position = playerPosition;
                break;
            case 2:
                playerPosition.x = 18.499f; //village
                playerPosition.y = 125.258f;
                player.transform.position = playerPosition;
                break;
            case 3:
                playerPosition.x = 9.874f; //village (chalet)
                playerPosition.y = 154.96f;
                player.transform.position = playerPosition;
                break;
            case 4:
                playerPosition.x = 43.184f; //Mare au canard (centre de la map)
                playerPosition.y = 61.332f;
                player.transform.position = playerPosition;
                break;
        }

        player.GetComponent<PlayerHealth>().HealPlayer(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
