using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    private int currentSprite = 0;
    private float secondsPerFrame = 0.1f;
    private float timer = 0.0f;
    private bool launching = false;
    private float launchTimer = 0.0f;
    private float timeToLaunch = 2.0f;

    private GameObject upwardsIce;
    private Vector3 iceOffset = new Vector3(0.75f, 0.0f, 0.0f);

    private bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        upwardsIce = Resources.Load<GameObject>("Prefabs/UpwardsIce");
    }

    // Update is called once per frame
    void Update()
    {
        if(playing)
        {
            launchTimer += Time.deltaTime;
        }

        if(launchTimer >= timeToLaunch)
        {
            launching = true;
            launchTimer = 0.0f;
        }

        if(launching)
        {
            timer += Time.deltaTime;
            if(timer >= secondsPerFrame)
            {
                currentSprite = ++currentSprite % sprites.Length;
                GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];

                if(currentSprite == 0)
                {
                    launching = false;
                }
                else if(currentSprite == 5 && playing)
                {
                    LaunchIce();
                }

                timer = 0.0f;  
            }
        }
        
    }

    private void LaunchIce()
    {
        Instantiate(upwardsIce, transform.position + iceOffset, transform.rotation);
    }

    public void SetPlayState(bool playing)
    {
        this.playing = playing;
    }
}
