using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class InsertLevelTimeController : MonoBehaviour
{
    private const string url = "http://192.168.0.167/Progra2026/Solar_show/insert_level_time.php";

    public void SaveTime(int playerId, int levelId, int time, TMP_Text resultText)
    {
        StartCoroutine(SendRequest(playerId, levelId, time, resultText));
    }

    private IEnumerator SendRequest(int playerId, int levelId, int time, TMP_Text resultText)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_id", playerId);
        form.AddField("level_id", levelId);
        form.AddField("time", time);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);

                if (resultText != null)
                {
                    resultText.text = www.downloadHandler.text;
                }
            }
            else
            {
                Debug.Log("Error de conexión");
            }
        }
    }
}