using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private ToggleButtonContainer buttonContainer;
    public void OnClickStartButton()
    {
        if (buttonContainer == null)
        {
            Debug.Log("버튼 콘테이너 연결 확인");
            return;
        }

        if (buttonContainer.SelectedIndex < 0 || buttonContainer.SelectedIndex > 3)
        {
            Debug.Log("무기 선택하지 않음");
            return;
        }

        //Debug.Log("게임 시작 연결필요");
        GameObject sceneData = new GameObject();
        if (buttonContainer.SelectedIndex == 0)
        {
            sceneData.name = "Melee";
        }
        else if (buttonContainer.SelectedIndex == 1)
        {
            sceneData.name = "Projectile";
        }
        else if ( buttonContainer.SelectedIndex == 2)
        {
            sceneData.name = "Chasing";
        }
        else
        {
            Debug.Log("무기가 선택되지 않음");
        }
        DontDestroyOnLoad(sceneData);
        SceneManager.LoadScene("Stage");

    }
}
