using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class GetPlayerTimeController : MonoBehaviour
{
    private const string url = "http://192.168.0.167/Progra2026/Solar_show/get_player_total_time.php";

    public void GetPlayerTime(TMP_Text resultText)
    {
        StartCoroutine(SendRequest(resultText));
    }

    private IEnumerator SendRequest(TMP_Text resultText)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_id", PlayerIdManager.currentPlayerId);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                GetPlayerTimeModel response = JsonUtility.FromJson<GetPlayerTimeModel>(www.downloadHandler.text);

                if (resultText != null)
                {
                    resultText.text = "Tiempo total: " + response.data;
                }
            }
            else
            {
                Debug.Log("Error de conexión");
            }
        }
    }
}