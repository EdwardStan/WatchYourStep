using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    public void ButtonManager(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0:

                SceneManager.LoadScene(1);
                break;
            case 1:
                Application.Quit();
                break;

        }
    }
}
