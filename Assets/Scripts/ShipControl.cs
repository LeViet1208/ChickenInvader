using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public static ShipControl Instance;
    [SerializeField] private GameObject ShipPreFaps;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RespawnShip()
    {
        var newShip = Instantiate(ShipPreFaps, Camera.main.ViewportToScreenPoint(new Vector3(0.5f, -0.5f, 0)), Quaternion.identity);
        var point = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 0));
        point.z = 0;
        StartCoroutine(MoveShipToPoint(newShip, point));
    }

    IEnumerator MoveShipToPoint(GameObject ship, Vector3 point)
    {
        float timer = 0;
        while (ship && ship.transform.position != point)
        {
            timer += Time.fixedDeltaTime;

            ship.transform.position = Vector3.Lerp(ship.transform.position, point, timer);
            yield return new WaitForFixedUpdate();
        }
    }
}
