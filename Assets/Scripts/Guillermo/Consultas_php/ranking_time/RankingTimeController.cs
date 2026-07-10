using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class RankingTimeController : MonoBehaviour
{
    private const string url = "http://192.168.0.167/Progra2026/Solar_show/ranking_time.php";

    public void GetRanking(TMP_Text resultText)
    {
        StartCoroutine(SendRequest(resultText));
    }

    private IEnumerator SendRequest(TMP_Text resultText)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                RankingTimeModel response = JsonUtility.FromJson<RankingTimeModel>(www.downloadHandler.text);

                string result = "RANKING DE TIEMPO\n\n";
                foreach (RankingTimeEntry entry in response.data)
                {
                    result += "Jugador " + entry.player_id + " - " + entry.total_time + "s\n";
                }

                if (resultText != null)
                {
                    resultText.text = result;
                }
            }
            else
            {
                Debug.Log("Error de conexión");
            }
        }
    }
}