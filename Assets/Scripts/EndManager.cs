using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    private DataStorage storage;
    private float time;
    private GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        storage = GameObject.Find("DataStorage").GetComponent<DataStorage>();
        time = storage.time;
        scoreText = GameObject.Find("Score Text");
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Your completion time was " + time.ToString("#.00") + " seconds!";
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
