using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] Animator fade;
    [SerializeField] float WaitTime;
    [SerializeField] string NextScene;
    public bool ChangeLevel = false;

    [SerializeField] bool ActiveOnTrigger;

    [SerializeField] GameObject LoadImage;
    float rotation;
    [SerializeField] Pausa pause;

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ActiveOnTrigger)
        {
            ChangeLevel = true;
        }
    }

    IEnumerator ChangeScene()
    {
        pause.CanUsePause = false;
        Time.timeScale = 1;
        fade.Play("Fade-Out");

        yield return new WaitForSeconds(1.5f);

        LoadImage.SetActive(true);

        yield return new WaitForSeconds(WaitTime);

        SceneManager.LoadScene(NextScene);
    }
}
