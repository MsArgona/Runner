using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform nextPos;

    void Start()
    {
        //если не выбрана точка телепортации, то телепортнет сюда же
        if (nextPos == null)
            nextPos = gameObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          other.GetComponent<PlayerController>().ChangePos(nextPos);
        }
    }
}
