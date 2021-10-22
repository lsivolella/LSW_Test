using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebRequester;

public class DataManager : MonoBehaviour
{
    [SerializeField] CSVHandlerRunTime CSVHandler;
    [SerializeField] private List<DataPanelMenu> dataPanels;
    [SerializeField] private List<DataTestSO> dataTestFiles;

    public List<DataTestSO> DataTestFiles => dataTestFiles;

    public void UpdateData()
    {
        for (int i = 0; i < dataPanels.Count; i++)
        {
            dataPanels[i].NameText.text = dataTestFiles[i].Name;
            dataPanels[i].PriceText.text = dataTestFiles[i].Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            dataPanels[i].BuffText.text = dataTestFiles[i].Buff.ToString();
            dataPanels[i].TypeText.text = dataTestFiles[i].Type.ToString();
        }
    }

    public void DownloadData()
    {
        StartCoroutine(CSVDownloaderRunTime.DownloadCSVInternal(CSVHandler.HandleDownloadedCSV, HandleProcessedCSV));
    }

    private void HandleProcessedCSV(List<DataTestHandler> processedData)
    {
        for (int i = 0; i < DataTestFiles.Count; i++)
        {
            DataTestFiles[i].UpdateData(processedData[i]);
        }
    }
}
