﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePrefabs : MonoBehaviour {

   
    public List<GameObject> Planes = new List<GameObject>();

    public void setColor(int x , int y , int z)
    {

        if (x == 0)
            Planes[4].SetActive(true);
        else if (x == -2)
            Planes[5].SetActive(true);
        if (y == 0)
            Planes[0].SetActive(true);
        else if (y == -2)
            Planes[1].SetActive(true);
        if (z == 0)
            Planes[2].SetActive(true);
        else if (z == 2)
            Planes[3].SetActive(true);
    }
}
