using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceController : MonoBehaviour
{
    private int counter = 0;
    private int stationaryBuffer = 20;
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject targetZone;
    private bool melting = false;

    private const float TARGET_DISTANCE = 0.15f;
    [SerializeField]
    private GameObject gameController;
    [SerializeField]
    private GameObject guides;
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //gameController = GameObject.Find("GameController");
        //targetZone = GameObject.Find("TargetZone");
        
        if(targetZone == null)
            melting = true;
    }

    // Update is called once per frame
    void Update()
    {       
        if(melting)
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            if(transform.localScale.x <= 0)
                Destroy(gameObject);
            return;
        }

        // Increment or reset the counter, depending on whether or not the ice is moving
        counter = (rb.velocity.magnitude == 0) ? counter + 1 : 0;

        // Once the ice has been stationary for the requisite # of frames, check
        if(counter >= stationaryBuffer)
        {
            // If in the target zone, lock it in place
            if(IsInside(targetZone))
            {
                rb.bodyType = RigidbodyType2D.Static;
                guides.GetComponent<GuideTrajectory>().IncreaseScore();
                Destroy(this);
            }
            // If not, melt the ice
            else
            {
                melting = true;
            }
        }
    }

    private bool IsInside(GameObject target)
    {
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if(distance <= TARGET_DISTANCE)
        {
            gameController.GetComponent<GameController>().NextTarget();
            return true;
        }

        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.CompareTag("Ground"))
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>(), true);
        }
    }

    public void SetVariables(GameObject gameController, GameObject targetZone, GameObject guides, GameObject player)
    {
        this.gameController = gameController;
        this.targetZone = targetZone;
        this.guides = guides;
        this.player = player;
    }
}
