using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TMPro.TMP_Text timerText;
    private float currentTime = 0.0f;
    private bool playing = false;
    [SerializeField]
    private GameObject targetZone;
    private int currentTargetPos = 0;
    private Vector3[] targetPositions;
    private Vector3[] targetPositionsIgloo = {new Vector3(8.257f, -0.08f, 0),
                                            new Vector3(7.722f, -0.08f, 0),
                                            new Vector3(7.187f, -0.08f, 0),
                                            new Vector3(6.644f, -0.08f, 0),
                                            new Vector3(6.101f, -0.08f, 0),
                                            new Vector3(5.558f, -0.08f, 0),
                                            new Vector3(6.901f, 0.463f, 0),
                                            new Vector3(6.348f, 0.463f, 0),
                                            new Vector3(5.805f, 0.463f, 0),
                                            new Vector3(6.604f, 1.006f, 0),
                                            new Vector3(6.056f, 1.006f, 0)};
    private Vector3[] targetPositionsSnowman = {new Vector3(8.07f, -0.08f, 0),
                                            new Vector3(7.526f, -0.08f, 0),
                                            new Vector3(6.981f, -0.08f, 0),
                                            new Vector3(6.427f, -0.08f, 0),
                                            new Vector3(8.07f, 0.463f, 0),
                                            new Vector3(7.526f, 0.463f, 0),
                                            new Vector3(6.981f, 0.463f, 0),
                                            new Vector3(6.427f, 0.463f, 0),
                                            new Vector3(8.07f, 1.006f, 0),
                                            new Vector3(7.526f, 1.006f, 0),
                                            new Vector3(6.981f, 1.006f, 0),
                                            new Vector3(6.427f, 1.006f, 0),
                                            new Vector3(7.526f, 1.549f, 0),
                                            new Vector3(6.981f, 1.549f, 0),
                                            new Vector3(7.526f, 2.092f, 0),
                                            new Vector3(6.981f, 2.092f, 0)};
    int level = 0;

    [SerializeField]
    private TMPro.TMP_Text inst_text;
    private int inst_step = 0;
    private float inst_timer = 0.0f;
    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject guides;
    [SerializeField]
    private GameObject snowman;

    // Start is called before the first frame update
    void Start()
    {
        level = DataStorage.instance.level;
        switch(level)
        {
            case 0: targetPositions = targetPositionsIgloo; break;
            case 1: targetPositions = targetPositionsSnowman; break;
        }

        targetZone.transform.position = targetPositions[0];
        inst_text.text = "Move right by pressing the D key!";
    }

    // Update is called once per frame
    void Update()
    {
        if(playing)
        {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("#.00");
            
            if(inst_step == 3)
            {
                if(inst_timer >= 10)
                {
                    UpdateInstructions();
                }
                else
                {
                    inst_timer += Time.deltaTime;
                }
            } 
            
            return;
        }

        if(Input.GetAxis("Horizontal") > 0 && inst_step == 0)
        {
            UpdateInstructions();
        }
        else if(Input.GetAxis("Horizontal") < 0 && inst_step == 1)
        {
            UpdateInstructions();
        }
    }

    void Win()
    {
        SetPlayState(false);
        targetZone.SetActive(false);
        DataStorage.instance.time = currentTime;
        foreach(SpriteRenderer renderer in guides.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = false;
        }
        backButton.SetActive(true);
        UpdateInstructions();
    }

    public void NextTarget()
    {
        // Play correct SFX

        // Update target position
        if(++currentTargetPos >= targetPositions.Length)
        {
            Win();
        }
        else
        {
            targetZone.transform.position = targetPositions[currentTargetPos];

            if(currentTargetPos == 1)
                UpdateInstructions();
        }
    }

    void SetPlayState(bool playing)
    {
        this.playing = playing;
        snowman.GetComponent<SnowmanController>().SetPlayState(playing);
    }

    void UpdateInstructions()
    {
        switch(++inst_step)
        {
            case 1:
                inst_text.text = "Move left by pressing the A key!";
                break;
            case 2:
                inst_text.text = "Bounce the ice to the target on the right. Use your mouse to aim!";
                targetZone.GetComponentInChildren<SpriteRenderer>().enabled = true;
                foreach(SpriteRenderer renderer in guides.GetComponentsInChildren<SpriteRenderer>())
                {
                    renderer.enabled = true;
                }
                SetPlayState(true);
                break;
            case 3:
                inst_text.text = "The guide will shrink as you progress. Good luck!";
                break;
            case 4:
                inst_text.text = "";
                break;
            case 5:
                inst_text.text = "You win! Thanks for playing! \n Your completion time was " + currentTime.ToString("#.00") + " seconds!";
                break;
        }
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
    
}
