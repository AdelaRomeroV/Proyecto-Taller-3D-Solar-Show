using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] Animator fade;
    [SerializeField] float WaitTime;
    [SerializeField] string NextScene;

    [SerializeField] GameObject LoadImage;
    float rotation;
    [SerializeField] Pausa pause;
    public bool ChangeLevel = false;

    private void Update()
    {
        if (ChangeLevel)
        {
            StartCoroutine(ChangeScene());
        }

        if (LoadImage.activeSelf)
        {
            rotation += 5;
            LoadImage.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }

    IEnumerator ChangeScene()
    {
        pause.CanUsePause = false;
        Time.timeScale = 1;

        yield return new WaitForSeconds(0.5f);
        fade.Play("Fade-Out");

        yield return new WaitForSeconds(1.5f);
        LoadImage.SetActive(true);

        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene(NextScene);
    }

    public void changeLevel(bool flag)
    {
        ChangeLevel = flag;
    }
}
