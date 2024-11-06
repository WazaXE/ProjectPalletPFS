using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    //Temp System Due to bug with UI Buttons
    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            ChangeScene();
        }


        if (Input.GetButtonDown("Reset"))
        {
            QuitGame();
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
