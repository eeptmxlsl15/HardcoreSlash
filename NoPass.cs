using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPass : MonoBehaviour
{
    RectTransform rect;
    public Player player;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (player.transform.childCount < 4)
        {
            rect.localScale = Vector3.zero;
        }
        else
            rect.localScale = Vector3.one;

    }


}
