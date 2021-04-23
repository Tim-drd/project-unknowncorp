using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobSpawners : MonoBehaviour
{
    public GameObject mob;
    public float x;
    public float y;
    public float max_x; //Il faut renseigner dans unity les coordonnées des 4 coins du carré représetant la zone de spawn;
    public float min_x;
    public float max_y;
    public float min_y;
    public int enemyCounter;
    public int enemyMaxCount; //Le nombre d'enemy qu'on veut au maximum dans une zone qui peut être variable selon la zone;
    private int _time = 1; //sinon y fait plusieurs boucles en pa
    

    // Update is called once per frame
    void Update()
    {
        GameObject[] N_mobs = GameObject.FindGameObjectsWithTag(mob.tag);
        enemyCounter = N_mobs.Length - 1;
        if (enemyCounter < enemyMaxCount && _time % Random.Range(650, 1100) == 0)
        {
            StartCoroutine(enemySpawn());
        }
        _time++;
    }

    private IEnumerator enemySpawn()
    {
        x = Random.Range(min_x, max_x);
        y = Random.Range(min_y, max_y);
        PhotonNetwork.Instantiate(mob.name, new Vector3(x, y, 0), Quaternion.identity);
        enemyCounter++;
        yield break;
    }
}
