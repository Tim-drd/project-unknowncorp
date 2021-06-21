using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateWallSpawn : MonoBehaviour
{
    public BoxCollider2D[] walls; //y faut les mettre dans le bonne ordre;
    public GameObject[] pnjs; //dans le bonne ordre;
    public int checkpoint_number;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("nb de murs: " + walls.Length);
        checkpoint_number = photonButtons.photonbutton.level;
        //permet de désactiver les différents murs en fonction de l'avancement de la partie.
        if (checkpoint_number > 0)
        {
           
            walls[0].enabled = false;
        }

        if (checkpoint_number > 1)
        {
           
            walls[1].enabled = false;
            walls[2].enabled = false;
        }
        if (checkpoint_number > 2)
        {
         
            pnjs[3].SetActive(false);
            pnjs[4].SetActive(true);
            walls[3].enabled = false;
            walls[4].enabled = false;
        }
        if (checkpoint_number > 3)
        {  
            walls[5].enabled = false;
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
        switch (checkpoint_number) //pour attribuer les tridents suivant la progression de la save;
        {
            case 1:
                foreach (var player in players)
                {
                    player.GetComponent<Animator>().SetInteger("weaponIndex", 1);
                }
                break;
            case 2:
                foreach (var player in players)
                {
                    player.GetComponent<Animator>().SetInteger("weaponIndex", 1);
                    player.GetComponent<Animator>().SetInteger("weaponIndex", 2);
                }
                break;
            case 3:
                foreach (var player in players)
                {
                    player.GetComponent<Animator>().SetInteger("weaponIndex", 1);
                    player.GetComponent<Animator>().SetInteger("weaponIndex", 2);
                    player.GetComponent<Animator>().SetInteger("weaponIndex", 3);
                }
                break;
            default:
                break;
        }
    }

}
