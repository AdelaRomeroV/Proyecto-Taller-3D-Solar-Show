using UnityEngine;

public class PlayerIdManager : MonoBehaviour
{
    public static int currentPlayerId;

    private CreatePlayerController controller;

    void Awake()
    {
        controller = GetComponent<CreatePlayerController>();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("player_id"))
        {
            currentPlayerId = PlayerPrefs.GetInt("player_id");
            Debug.Log("Jugador existente, ID: " + currentPlayerId);
        }
        else
        {
            controller.CreateNewPlayer(this);
        }
    }

    public void SetNewPlayerId(int newPlayerId)
    {
        currentPlayerId = newPlayerId;
        PlayerPrefs.SetInt("player_id", currentPlayerId);
        PlayerPrefs.Save();
    }
}
