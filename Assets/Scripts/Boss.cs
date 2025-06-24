using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject EggPreFaps;
    [SerializeField] private int HP = 100;
    [SerializeField] private GameObject DestroyEffect;
    [SerializeField] private float Speed = 7;
    [SerializeField] TMP_Text HpText;
    [SerializeField] private GameObject ReplayButton;
    public static Boss instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEgg());
        StartCoroutine(MoveBossToRandomPoint());
    }

    public void PutDmg(int dmg)
    {
        HP -= dmg;
        HpText.text = "HP : " + HP.ToString();

        if (HP <= 0)
        {
            Destroy(gameObject);
            var effect = Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1);
            HpText.gameObject.SetActive(false);

            ReplayButton.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    IEnumerator SpawnEgg()
    {
        while (true)
        {
            Instantiate(EggPreFaps, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        }
    }

    IEnumerator MoveBossToRandomPoint()
    {
        Vector3 point = getRandomPoint();
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, Speed * Time.fixedDeltaTime);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(MoveBossToRandomPoint());
    }

    Vector3 getRandomPoint()
    {
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1f)));
        posRandom.z = 0;
        return posRandom;
    }

    public void DisplayHP()
    {
        HpText.gameObject.SetActive(true);
        HpText.text = "HP : " + HP.ToString();
    }
}
