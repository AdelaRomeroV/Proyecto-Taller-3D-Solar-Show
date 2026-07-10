using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class GetBestTimeLevelController : MonoBehaviour
{
    private const string url = "http://192.168.0.167/Progra2026/Solar_show/get_best_time_level.php";

    public void GetBestTime(int levelId, TMP_Text resultText)
    {
        StartCoroutine(SendRequest(levelId, resultText));
    }

    private IEnumerator SendRequest(int levelId, TMP_Text resultText)
    {
        WWWForm form = new WWWForm();
        form.AddField("level_id", levelId);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                GetBestTimeLevelModel response = JsonUtility.FromJson<GetBestTimeLevelModel>(www.downloadHandler.text);

                if (resultText != null)
                {
                    resultText.text = "Mejor tiempo: " + response.data;
                }
            }
            else
            {
                Debug.Log("Error de conexión");
            }
        }
    }
}