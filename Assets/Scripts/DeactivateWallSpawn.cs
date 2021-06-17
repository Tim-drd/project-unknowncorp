using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateWallSpawn : MonoBehaviour
{



    public BoxCollider2D wall1;
    public BoxCollider2D wall2;
    public BoxCollider2D wall3;
    public BoxCollider2D wall4;
    public int checkpoint_number;

    // Start is called before the first frame update
    void Start()
    {
        checkpoint_number = photonButtons.photonbutton.level;


        //permet de désactiver les différents murs en fonction de l'avancement de la partie.
        if (checkpoint_number > 0)
            wall1.enabled = false;

        if(checkpoint_number > 1)
            wall2.enabled = false;

        if (checkpoint_number > 2)
            wall3.enabled = false;

    }

}
