using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottonBlock : MonoBehaviour
{
    public Button button;

    [SerializeField] public int levels;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
    }
}
