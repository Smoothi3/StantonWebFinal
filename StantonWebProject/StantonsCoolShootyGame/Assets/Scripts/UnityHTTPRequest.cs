using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnityHTTPRequest : MonoBehaviour
{
    public InputField displayField;

    public InputField[] highScores;

    bool repeated = false;

    public string nameData;
    //Change these inputs later
    public string nameInput = "";
    public int scoreInput = 0;
    public int playsInput = 0;

    [SerializeField]

    string postData;

    [Serializable]
    public class MyData
    {
        public string myName;
        public int myScore;
        public int myPlays;
    }

    public void Start()
    {
        nameInput = ScrGameManager.playerName;
        scoreInput = ScrGameManager.score;
        playsInput = ScrGameManager.timesPlayed;

        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            //Send data
            SendOrUpdate();
        } else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            //Retrieve data
            GetMultipleData();
        }
    }

    IEnumerator MakeWebRequest()
    {
        var getRequest = new UnityWebRequest($"http://localhost:3000/unity?name={nameData}");
        getRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return getRequest.SendWebRequest();
        Debug.Log(getRequest.result);
    }

    IEnumerator PostRequest(string sendData)
    {
        var request = new UnityWebRequest("http://localhost:3000/unityPost", "POST");
        byte[] bodyData = Encoding.UTF8.GetBytes(sendData);
        request.uploadHandler = new UploadHandlerRaw(bodyData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
    }

    IEnumerator MakeWebRequestSingle()
    {
        //GET Request Example with Query
        string searchName = nameInput;
        var getRequest = new UnityWebRequest($"http://localhost:3000/unityGetOne?myName={searchName}");
        getRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return getRequest.SendWebRequest();
        Debug.Log(getRequest.result);

        switch (getRequest.result)
        {
            case UnityWebRequest.Result.Success:
                if (getRequest.downloadHandler.text == "null")
                {
                    repeated = false;
                    //singleSearchBox.text = "Name not found, try again";
                }
                else
                {
                    repeated = true;
                    Debug.Log(getRequest.downloadHandler.text);
                    //singleSearchBox.text = getRequest.downloadHandler.text;
                }
                break;
        }



        //CreateSingleObjectFromData(getRequest);
        //CreateObjectsFromArray(jsonData);
    }

    IEnumerator MakeWebRequestMultiple()
    {
        var getRequest = new UnityWebRequest($"http://localhost:3000/unity");
        getRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return getRequest.SendWebRequest();
        Debug.Log(getRequest.result);


        switch (getRequest.result)
        {
            case UnityWebRequest.Result.Success:
                Debug.Log(getRequest.downloadHandler.text);
                displayField.text = getRequest.downloadHandler.text;

                string regularData = getRequest.downloadHandler.text;
                string dataParse = regularData.Trim(new char[] { '{', '}', '[', ']' });
                string[] furtherParse = dataParse.Split('}');
                Debug.Log(dataParse);
                List<string[]> parses = new List<string[]>();
                for(int i = 0; i < furtherParse.Length; i++)
                {
                    furtherParse[i] = furtherParse[i].Remove(0, 2);
                    string[] indiParse = furtherParse[i].Split(',');
                    parses.Add(indiParse);
                    //Debug.Log(furtherParse[i]);
                }

                int loopCount = 10;
                if(parses.Count < 10)
                {
                    loopCount = parses.Count;
                }

                for(int i = 0; i < loopCount; i++)
                {
                    string[] nameSplit = parses[i][1].Split(':');
                    string playerName = nameSplit[1].Trim(new char[] { '"' });
                    //nameSplit[1] = nameSplit[1].Trim(new char[] { '"' });

                    string[] scoreSplit = parses[i][2].Split(':');
                    string playerScore = scoreSplit[1].Trim(new char[] { '"' });
                    //scoreSplit[1] = scoreSplit[1].Trim(new char[] { '"' });

                    highScores[i].text = playerName + " --- " + playerScore;
                    Debug.Log(playerName + " --- " + playerScore);
                }
                //multiSearchBox.text = getRequest.downloadHandler.text;
                break;
        }
    }

    
    IEnumerator MakeDeleteRequest()
    {
        //Check if entry exists
        string searchName = nameInput;

        var searchRequest = new UnityWebRequest($"http://localhost:3000/unityGetOne?myName={searchName}");
        searchRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return searchRequest.SendWebRequest();

        switch (searchRequest.result)
        {
            case UnityWebRequest.Result.Success:
                if (searchRequest.downloadHandler.text == "null")
                {
                    Debug.Log("Name not found, try again");
                }
                else
                {
                    Debug.Log("Deleting " + searchName);
                    var getRequest = new UnityWebRequest($"http://localhost:3000/unityDeleteEntry?myName={searchName}", "POST");
                    getRequest.downloadHandler = new DownloadHandlerBuffer();
                    yield return getRequest.SendWebRequest();
                }
                break;
        }

    }


    IEnumerator UpdateOrAddRequest(string sendData)
    {
        string searchName = nameInput;

        var searchRequest = new UnityWebRequest($"http://localhost:3000/unityGetOne?myName={searchName}");
        searchRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return searchRequest.SendWebRequest();

        switch (searchRequest.result)
        {
            case UnityWebRequest.Result.Success:
                if(searchRequest.downloadHandler.text == "null")
                {
                    //Instance does not exist, create a new instance
                    Debug.Log("New entry found");
                    var request = new UnityWebRequest("http://localhost:3000/unityPost", "POST");
                    byte[] bodyData = Encoding.UTF8.GetBytes(sendData);
                    request.uploadHandler = new UploadHandlerRaw(bodyData);
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SetRequestHeader("Content-Type", "application/json");
                    yield return request.SendWebRequest();
                } else
                {
                    //Instance exists, update it
                    Debug.Log("Previous entry found, updating...");

                    string dataStuff = searchRequest.downloadHandler.text;
                    string[] dataSuffList = dataStuff.Split(',');

                    List<string> furtherParse = new List<string>();
                    string[] nameData = dataSuffList[1].Split(':');
                    string[] scoreData = dataSuffList[2].Split(':');
                    string[] playsData = dataSuffList[3].Split(':');
                    furtherParse.Add(nameInput);
                    furtherParse.Add(scoreData[1]);
                    furtherParse.Add(playsData[1]);

                    for(int i = 0; i < furtherParse.Count; i++)
                    {
                        Debug.Log(furtherParse[i]);
                    }

                    MyData newData = new MyData();

                    newData.myName = furtherParse[0];

                    if(scoreInput > int.Parse(furtherParse[1]))
                    {
                        Debug.Log("Score higher");
                        newData.myScore = scoreInput;
                    } else
                    {
                        Debug.Log("Score lower");
                        newData.myScore = int.Parse(furtherParse[1]);
                    }

                    newData.myPlays = int.Parse(furtherParse[2]) + 1;

                    var postData = JsonUtility.ToJson(newData);
                    Debug.Log(postData);

                    var putRequest = new UnityWebRequest("http://localhost:3000/unityUpdate?myName={searchName}", "PUT");
                    byte[] bodyData = Encoding.UTF8.GetBytes(postData);
                    putRequest.uploadHandler = new UploadHandlerRaw(bodyData);
                    putRequest.downloadHandler = new DownloadHandlerBuffer();
                    putRequest.SetRequestHeader("Content-Type", "application/json");
                    yield return putRequest.SendWebRequest();
                }
                break;
        }
    }
    

    IEnumerator MakeUpdateRequest(string sendData)
    {
        //Check if entry exists
        string searchName = nameInput;

        var searchRequest = new UnityWebRequest($"http://localhost:3000/unityGetOne?myName={searchName}");
        searchRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return searchRequest.SendWebRequest();

        switch (searchRequest.result)
        {
            case UnityWebRequest.Result.Success:
                if (searchRequest.downloadHandler.text == "null")
                {
                    Debug.Log("Name not found, try again");
                }
                else
                {
                    Debug.Log("Updating " + searchName);
                    var putRequest = new UnityWebRequest("http://localhost:3000/unityUpdate", "PUT");
                    byte[] bodyData = Encoding.UTF8.GetBytes(sendData);
                    putRequest.uploadHandler = new UploadHandlerRaw(bodyData);
                    putRequest.downloadHandler = new DownloadHandlerBuffer();
                    putRequest.SetRequestHeader("Content-Type", "application/json");
                    yield return putRequest.SendWebRequest();
                }
                break;
        }
    }

    private static void CreateSingleObjectFromData(UnityWebRequest getRequest)
    {
        var deserializedJson = JsonUtility.FromJson<MyData>(getRequest.downloadHandler.text);
        Debug.Log(deserializedJson.myName);
    }

    private void CreateObjectsFromArray(string jsonData)
    {
        string jsonString = fixJson(jsonData);
        MyData[] objData = JsonHelper.FromJson<MyData>(jsonString);
        //create data object array here
    }

    public void GetMultipleData()
    {
        StartCoroutine(MakeWebRequestMultiple());
    }

    public void GetSingleData()
    {
        StartCoroutine(MakeWebRequestSingle());
    }

    public void DeleteEntry()
    {
        StartCoroutine(MakeDeleteRequest());
    }

    public void SendData()
    {
        MyData sendData = new MyData();
        sendData.myName = nameInput;
        sendData.myScore = scoreInput;
        sendData.myPlays = playsInput;
        var postData = JsonUtility.ToJson(sendData);
        Debug.Log(postData);
        StartCoroutine(PostRequest(postData));
    }

    public void SendOrUpdate()
    {
        MyData sendData = new MyData();
        sendData.myName = nameInput;
        sendData.myScore = scoreInput;
        sendData.myPlays = playsInput;
        var postData = JsonUtility.ToJson(sendData);
        Debug.Log(postData);
        StartCoroutine(UpdateOrAddRequest(postData));
    }

    public void UpdateData()
    {
        MyData sendData = new MyData();
        sendData.myName = nameInput;
        sendData.myScore = scoreInput;
        sendData.myPlays = playsInput;
        var postData = JsonUtility.ToJson(sendData);
        Debug.Log(postData);
        StartCoroutine(MakeUpdateRequest(postData));
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            Debug.Log(wrapper.Items);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

}
