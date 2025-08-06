using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataFetcher : MonoBehaviour
{

    [SerializeField] TMP_Text playerNameText;
    [SerializeField] TMP_Text playerHealthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text playerPositionText;


    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] TMP_InputField playerHealthInput;
    [SerializeField] TMP_InputField scoreInput;
    [SerializeField] TMP_InputField playerPositionInputX;
    [SerializeField] TMP_InputField playerPositionInputY;
    [SerializeField] TMP_InputField playerPositionInputZ;

    [SerializeField] TMP_Text warningText;

    private string url = "https://localhost:7047/api/data";



    IEnumerator FetchData()
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            Data data = JsonUtility.FromJson<Data>(json);

            playerNameText.text = $"{data.playerName}";
            playerHealthText.text = $"{data.playerHealth}";
            scoreText.text = $"{data.score}";
            playerPositionText.text = $"X: {data.playerPosition.x}, Y: {data.playerPosition.y}, Z: {data.playerPosition.z}";
        }
    }

    IEnumerator PostData(Data data)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        string jsonData = JsonUtility.ToJson(data);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data saved!");
        }

    }

    public void ShowData()
    {
        StartCoroutine(FetchData());
    }

    public void SaveData()
    {
        if (
            !int.TryParse(playerHealthInput.text, out int playerHealth) ||
            !int.TryParse(scoreInput.text, out int scoreInt) ||
            !float.TryParse(playerPositionInputX.text, out float posX) ||
            !float.TryParse(playerPositionInputY.text, out float posY) ||
            !float.TryParse(playerPositionInputZ.text, out float posZ)
        )
        {
            warningText.text = "Please enter valid numbers.";
            return;
        }

        warningText.text = "Saved!"; // Clear warning if all values are valid

        Data data = new Data
        {
            playerName = playerNameInput.text,
            playerHealth = playerHealth,
            score = scoreInt,
            playerPosition = new PlayerPosition { x = posX, y = posY, z = posZ }
        };

        StartCoroutine(PostData(data));
    }

}
