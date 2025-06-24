using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    private float gridSize = 1;
    private Vector3 spawnPos;
    [SerializeField] private GameObject ChickenPreFaps;
    [SerializeField] private GameObject BossPreFaps;
    [SerializeField] private Transform GridChicken;
    private int ChickenCurrent;
    public static Spawner instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        spawnPos.x += ((gridSize / 2 + (width / 4)));
        spawnPos.y -= gridSize;
        spawnPos.z = 0;

        SpawnChicken(Mathf.FloorToInt(height / 2 / gridSize), Mathf.FloorToInt(width / gridSize / 1.5f));
    }

    void SpawnChicken(int row, int chickenPerRow)
    {
        float x = spawnPos.x;

        for (int i = 0; i < row; ++i)
        {
            for (int j = 0; j < chickenPerRow; ++j)
            {
                spawnPos.x += gridSize;
                GameObject Chicken = Instantiate(ChickenPreFaps, spawnPos, quaternion.identity);
                Chicken.transform.parent = GridChicken;
                ++ChickenCurrent;
            }
            spawnPos.x = x;
            spawnPos.y -= gridSize;
        }
    }

    public void DecChicken()
    {
        --ChickenCurrent;
        if (ChickenCurrent <= 0)
        {
            if (BossPreFaps != null) 
                BossPreFaps.gameObject.SetActive(true);
            Boss.instance.DisplayHP();
        }
    }
}
