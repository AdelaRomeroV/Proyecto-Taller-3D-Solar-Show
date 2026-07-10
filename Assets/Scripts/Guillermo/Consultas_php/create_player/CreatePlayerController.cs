using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CreatePlayerController : MonoBehaviour
{
    private const string url = "http://192.168.0.167/Progra2026/Solar_show/create_player.php";

    public void CreateNewPlayer(PlayerIdManager manager)
    {
        StartCoroutine(SendRequest(manager));
    }

    private IEnumerator SendRequest(PlayerIdManager manager)
    {
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(url, ""))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                CreatePlayerModel response = JsonUtility.FromJson<CreatePlayerModel>(www.downloadHandler.text);

                if (response.data > 0)
                {
                    manager.SetNewPlayerId(response.data);
                    Debug.Log("Nuevo jugador creado, ID: " + response.data);
                }
            }
            else
            {
                Debug.Log("Error de conexión al crear jugador");
            }
        }
    }
}
