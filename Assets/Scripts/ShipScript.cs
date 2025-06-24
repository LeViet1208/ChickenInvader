using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentBulletTier;
    [SerializeField] private GameObject DestroyEffect;
    [SerializeField] private GameObject Shield;
    [SerializeField] private int ScoreOfChickenLeg;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableShield());
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0);

        transform.position += direction.normalized * Time.deltaTime * Speed;

        Vector3 TopLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, TopLeftPoint.x * -1, TopLeftPoint.x),
            Mathf.Clamp(transform.position.y, TopLeftPoint.y * -1, TopLeftPoint.y));

        if (Input.GetMouseButtonDown(0)) Shot();
    }

    void Shot()
    {
        Instantiate(BulletList[CurrentBulletTier], transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Shield.activeSelf && (collision.CompareTag("Chicken") || collision.CompareTag("Egg")))
        {
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("ChickenLeg"))
        {
            Destroy(collision.gameObject);
            ScoreController.instance.GetScore(ScoreOfChickenLeg);
        }
    }

    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(8);
        Shield.SetActive(false);
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            var Effect = Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(Effect, 1f);
            ShipControl.Instance.RespawnShip();
        }
    }
}
