using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public AiController aiController;
    bool spawnAndChase = false;
    public int disappearTime = 5;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnAndChase == true)
        {
            aiController.ChasePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spawnAndChase = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            spawnAndChase = false;
        }

        if (aiController.gameObject.activeInHierarchy == true)
        {
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(disappearTime);
        aiController.gameObject.SetActive(false);
    }
}
