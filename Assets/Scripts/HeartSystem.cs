using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    [SerializeField] GameObject fullHeartPrefab;
    [SerializeField] GameObject halfHeartPrefab;
    [SerializeField] GameObject emptyHeartPrefab;

    public void DrawHeart(float health, int maxHearts)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        bool isFloat = (health % 1 != 0);

        Vector2 heartSize = new Vector2(30 * Screen.width / 800 , 30 * Screen.width / 800);

        for (int i = 0; i < maxHearts; i++)
        {
            if (i < health && i + 1 > health && isFloat)
            {
                GameObject heart = Instantiate(halfHeartPrefab, transform.position, Quaternion.identity);
                RectTransform rt = (RectTransform)heart.transform;
                rt.sizeDelta = heartSize;
                heart.transform.SetParent(transform);
            }
            else
            {
                if (i < health)
                {
                    GameObject heart = Instantiate(fullHeartPrefab, transform.position, Quaternion.identity);
                    RectTransform rt = (RectTransform)heart.transform;
                    rt.sizeDelta = heartSize;
                    heart.transform.SetParent(transform);
                }
                else
                {
                    GameObject heart = Instantiate(emptyHeartPrefab, transform.position, Quaternion.identity);
                    RectTransform rt = (RectTransform)heart.transform;
                    rt.sizeDelta = heartSize;
                    heart.transform.SetParent(transform);
                }
            }
        }
    }
}
