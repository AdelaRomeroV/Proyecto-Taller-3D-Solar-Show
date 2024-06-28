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
                
            }
            else if (MenuActive && Input.GetKeyDown(KeyCode.Escape))
            {
                Deactive();
                
            }
        }
    }

    void Active()
    {
        PausaUI.SetActive(true);
        MenuActive = true;
        Time.timeScale = 0f;
    }

    public void Deactive()
    {
        PausaUI.SetActive(false);
        MenuActive = false;
        Time.timeScale = 1.0f;
    }
}
