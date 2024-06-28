using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambioDeEscenas : MonoBehaviour
{
    public Button button;
    public string sceneName;
    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
    }

    // Update is called once per frame
    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
