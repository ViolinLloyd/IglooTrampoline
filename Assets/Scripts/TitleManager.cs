using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private GameObject playBtn;

    [SerializeField]
    private GameObject instructions;
    [SerializeField]
    private GameObject level0Button;
    [SerializeField]
    private GameObject level1Button;

    private float timer = 0.0f;
    private float startPos = 89.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        title.transform.localPosition = new Vector3(0, startPos + 25 * Mathf.Sin(1.5f * timer), 0);
    }

    public void GoToLevelSelect()
    {
        title.SetActive(false);
        playBtn.SetActive(false);

        instructions.SetActive(true);
        level0Button.SetActive(true);
        level1Button.SetActive(true);
    }

    public void GoToTitle()
    {
        title.SetActive(true);
        playBtn.SetActive(true);

        instructions.SetActive(false);
        level0Button.SetActive(false);
        level1Button.SetActive(false);
    }

    public void SelectLevel(int level)
    {
        DataStorage.instance.level = level;
        SceneManager.LoadScene("Main");
    }
}
