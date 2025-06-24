using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfDistance : MonoBehaviour
{
    [SerializeField] private float DestroyDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DestroyIfReachDistances();
    }
    
    private void DestroyIfReachDistances()
    {
        Vector3 CenterScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Vector3.Distance(CenterScreen, transform.position) > DestroyDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
