using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCheckpointSpawn : MonoBehaviour
{

    public static LoadCheckpointSpawn checkpointSpawn;
    public int checkpoint_number;

    private void Awake()
    {
        checkpointSpawn = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //pour que le joueur spawn au bon checkpoit lors du démarrage de la partie.
        checkpoint_number = photonButtons.photonbutton.level;
        Debug.Log("au spawn, checkpoint = " + checkpoint_number);
        Checkpoint.instance.spawnTo(checkpoint_number, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
