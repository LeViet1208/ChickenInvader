using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ChickenScript : MonoBehaviour
{

    [SerializeField] private GameObject EggPreFaps;
    [SerializeField] private int score;
    [SerializeField] private GameObject ChickenLegPreFaps;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEgg());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEgg()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 20));
            Instantiate(EggPreFaps, transform.position, Quaternion.identity);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            ScoreController.instance.GetScore(score);
            Instantiate(ChickenLegPreFaps, transform.position, Quaternion.identity);

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (this != null && Spawner.instance != null)
        {
                Spawner.instance.DecChicken();
        }

    }
}
