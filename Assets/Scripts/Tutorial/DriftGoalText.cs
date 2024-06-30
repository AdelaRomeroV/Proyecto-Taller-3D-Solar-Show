using TMPro;
using UnityEngine;

public class DriftGoalText : MonoBehaviour
{
    [SerializeField] ControladorTutorial controlador;
    [SerializeField] EnemiesControllerTutorial enemiesController;
    TextMeshProUGUI text;
    [SerializeField] bool isDrift;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (isDrift)
        {
            if(controlador.derrape <= 5)
            {
                text.text = $"Derrapes Existosos:{controlador.derrape}/5";
            }
        }
        else
        {
            if(enemiesController.navesDestruidas <= 6)
            {
                text.text = $"Naves Destruidas: {enemiesController.navesDestruidas}/6";
            }
        }
    }

    public void SAtext()
    {
        isDrift = false;
    }
}
