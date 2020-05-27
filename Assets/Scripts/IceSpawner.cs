using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawner : MonoBehaviour
{
    public GameObject leftBarrier;
    public GameObject rightBarrier;
    private GameObject ice;

    [SerializeField]
    private GameObject gameController;
    [SerializeField]
    private GameObject targetZone;
    [SerializeField]
    private GameObject guides;
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        ice = Resources.Load<GameObject>("Prefabs/Ice");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnIce()
    {
        float leftPos = leftBarrier.transform.position.x + 0.5f;
        float rightPos = rightBarrier.transform.position.x - 1.25f;
        float offset = Random.Range(0, rightPos - leftPos);
        transform.position = new Vector3(leftPos + offset, transform.position.y, transform.position.z);
        GameObject iceObj = Instantiate(ice, transform.position, transform.rotation);
        iceObj.GetComponent<IceController>().SetVariables(gameController, targetZone, guides, player);
    }
}
