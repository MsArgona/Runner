using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;
    public float smooth = 5.0f;
    public Vector3 offset = new Vector3(0, 2, -5);

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("PLayer");
        }
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * smooth);
    }
}
