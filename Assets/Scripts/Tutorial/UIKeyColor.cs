using UnityEngine;
using UnityEngine.UI;

public class UIKeyColor : MonoBehaviour
{
    ControladorTutorial controlador;
    [SerializeField] Turbo turbo;
    [SerializeField] Mov mov;

    [Header("Movimiento")]
    [SerializeField] Image W;
    [SerializeField] Image Right;
    [SerializeField] Image Left;

    [Header("Turbo")]
    [SerializeField] GameObject Turbo;

    [Header("SideAttack")]
    [SerializeField] GameObject A;
    [SerializeField] GameObject D;

    Color ImageGray = Color.gray;

    private void Start()
    {
        controlador = GetComponent<ControladorTutorial>();
    }

    private void Update()
    {
        if (mov.enabled)
        {
            if (controlador.W_pressed) W.color = ImageGray;
            if (controlador.D_pressed) Right.color = ImageGray;
            if (controlador.A_pressed) Left.color = ImageGray;

        }
        if (turbo.TurboActive) Turbo.SetActive(false);
        if (controlador.Left_Pressed) A.SetActive(false);
        if (controlador.Right_Pressed) D.SetActive(false);
    }
}
