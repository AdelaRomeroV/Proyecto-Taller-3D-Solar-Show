using UnityEngine;
using UnityEngine.UI;

public class UIKeyColor : MonoBehaviour
{
    ControladorTutorial controlador;
    Turbo turbo;

    [Header("Movimiento")]
    [SerializeField] GameObject W;
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
        turbo = GameObject.FindGameObjectWithTag("Player").GetComponent<Turbo>();
    }

    private void Update()
    {
        if (controlador.W_pressed) W.SetActive(false);
        if (controlador.Right_pressed) Right.color = ImageGray;
        if (controlador.Left_pressed) Left.color = ImageGray;
        if (turbo.TurboActive) Turbo.SetActive(false);
        if (controlador.A_Pressed) A.SetActive(false);
        if (controlador.D_Pressed) D.SetActive(false);
    }
}
