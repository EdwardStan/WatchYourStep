using UnityEngine;

public class FirstTimeTutorial : MonoBehaviour
{
    public GameObject TutorialPanel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            TutorialPanel.SetActive(true);
        }
    }

    public void TutorialDone()
    {
        TutorialPanel.SetActive(false);
        PlayerPrefs.SetInt("Tutorial", 1);
    }

}
