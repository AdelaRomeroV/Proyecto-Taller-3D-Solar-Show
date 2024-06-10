using UnityEngine;

public class Pausa : MonoBehaviour
{
    [SerializeField] GameObject PausaUI;
    bool MenuActive = false;
    public bool CanUsePause;

    private void Start()
    {
        PausaUI.SetActive(false);
    }

    private void Update()
    {
        if (CanUsePause)
        {
            if (!MenuActive && Input.GetKeyDown(KeyCode.Escape))
            {
                Active();
                MenuActive = true;
            }
            else if (MenuActive && Input.GetKeyDown(KeyCode.Escape))
            {
                Deactive();
                MenuActive = false;
            }
        }
    }

    void Active()
    {
        PausaUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Deactive()
    {
        PausaUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
