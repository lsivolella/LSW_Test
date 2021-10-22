using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace WebRequester
{
    public static class CSVDownloaderRunTime
    {
        private const string googleSheetDocID = "16V7paPeeNuKfh0rU6IrFjWOKL9rC4d8WIY7qcgQQZ5o";
        private const string doc_url = "https://docs.google.com/spreadsheets/d/" + googleSheetDocID + "/export?format=csv";

        internal static IEnumerator DownloadCSVInternal(Action<string, Action<List<DataTestHandler>>> onCompleted, Action<List<DataTestHandler>> onCSVProcessed)
        {
            yield return new WaitForEndOfFrame();

            string downloadedData = null;

            using (UnityWebRequest webRequest = UnityWebRequest.Get(doc_url))
            {
                Debug.Log("Starting Download...");
                yield return webRequest.SendWebRequest();
                
                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("...Download Error: " + webRequest.error);
                }
                else
                {
                    downloadedData = webRequest.downloadHandler.text;
                    Debug.Log("...Downloaded Data");
                    Debug.Log("Data: " + downloadedData);
                    onCompleted(downloadedData, onCSVProcessed);
                }
            }
        }

        //private static int ExtractEqualsIndex(DownloadHandler downlaodHandler)
        //{
        //    if (downlaodHandler.text == null || downlaodHandler.text.Length < 10)
        //    {
        //        return -1;
        //    }

        //}
    }
}
