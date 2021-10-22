using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WebRequester;

public class CSVHandlerRunTime : MonoBehaviour
{
    [SerializeField] DataManager dataManager;

    // List<Row>
    // Row = List<string>
    private readonly List<DataTestHandler> downloadedEntries = new List<DataTestHandler>();

    public void HandleDownloadedCSV(string data, Action<List<DataTestHandler>> onCSVProcessed)
    {
        if (data == null)
            Debug.LogError("Could not download the data. Try again...");
        else
            StartCoroutine(ProcessData(data, onCSVProcessed));
    }

    private IEnumerator ProcessData(string data, Action<List<DataTestHandler>> onCompleted)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        // Line info
        int currentLineIndex = 0;
        bool inQuote = false;
        List<string> currentLineEntries = new List<string>();

        // Entry level
        StringBuilder currentEntry = new StringBuilder();
        int currentCharIndex = 0;
        bool currentEntryContainedQuote = false;

        // Line ending specs
        char lineEndingChar = Environment.NewLine[0];
        int lineEndingLength = Environment.NewLine.Length;

        while (currentCharIndex <= data.Length)
        {
            if (currentCharIndex == data.Length)
            {
                TrimQuotesFromEntry(currentEntry, ref currentEntryContainedQuote);

                ProcessEntry(currentLineEntries, currentEntry);

                // Line ended
                ProcessLineFromCSV(currentLineEntries, currentLineIndex);
                currentCharIndex++;
            }
            else if (!inQuote && data[currentCharIndex] == lineEndingChar)
            {
                // Skip line ending char
                currentCharIndex += lineEndingLength;

                TrimQuotesFromEntry(currentEntry, ref currentEntryContainedQuote);

                ProcessEntry(currentLineEntries, currentEntry);

                // Line ended
                ProcessLineFromCSV(currentLineEntries, currentLineIndex);
                currentLineIndex++;
                currentLineEntries = new List<string>();
            }
            else
            {
                if (data[currentCharIndex] == '"')
                {
                    // Check if quotes are part of the text of if they are present for CSV formatting
                    int tempIndex = 0;
                    while (currentCharIndex + tempIndex < data.Length 
                        && data[currentCharIndex + tempIndex] == '"')
                    {
                        tempIndex++;
                    }
                    // If three quotes (temIndex == 3) were found one quote should be kept in the final result
                    if (tempIndex == 3)
                    {
                        // Append the quote and move two indexes forward
                        currentEntry.Append(data[currentCharIndex]);
                        currentCharIndex += 2;
                    }

                    inQuote = !inQuote;
                    currentEntryContainedQuote = true;
                }
                if (data[currentCharIndex] == ',')
                {
                    if (inQuote)
                    {
                        currentEntry.Append(data[currentCharIndex]);
                    }
                    else
                    {
                        TrimQuotesFromEntry(currentEntry, ref currentEntryContainedQuote);

                        ProcessEntry(currentLineEntries, currentEntry);
                    }
                }
                else
                {
                    currentEntry.Append(data[currentCharIndex]);
                }
                currentCharIndex++;
            }
        }
        onCompleted(downloadedEntries);
        downloadedEntries.Clear();
    }

    private static void ProcessEntry(List<string> currentLineEntries, StringBuilder currentEntry)
    {
        // Add last entry found before cleaning entry buffer
        currentLineEntries.Add(currentEntry.ToString());
        currentEntry.Clear();
    }

    private static void TrimQuotesFromEntry(StringBuilder currentEntry, ref bool currentEntryContainedQuote)
    {
        if (!currentEntryContainedQuote) return;

        // If the current entry contains quotes, trim them out of the text
        string temp = currentEntry.ToString(1, currentEntry.Length - 2);
        currentEntry.Clear().Append(temp);
        currentEntryContainedQuote = false;
    }

    public IEnumerator ProcessDataOriginal(string data, System.Action<string> onCompleted)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        //Manager.instance.translator.termData = new TermData.Terms();

        // Line level
        int currLineIndex = 0;
        bool inQuote = false;
        int linesSinceUpdate = 0;
        int kLinesBetweenUpdate = 15;

        // Entry level
        string currEntry = "";
        int currCharIndex = 0;
        bool currEntryContainedQuote = false;
        List<string> currLineEntries = new List<string>();

        // "\r\n" means end of line and should be only occurence of '\r' (unless on macOS/iOS in which case lines ends with just \n)
        char lineEnding = Environment.NewLine[0];
        int lineEndingLength = Environment.NewLine.Length;

        while (currCharIndex < data.Length)
        {
            if (!inQuote && (data[currCharIndex] == lineEnding))
            {
                // Skip the line ending
                currCharIndex += lineEndingLength;

                // Wrap up the last entry
                // If we were in a quote, trim bordering quotation marks
                if (currEntryContainedQuote)
                {
                    currEntry = currEntry.Substring(1, currEntry.Length - 2);
                }

                currLineEntries.Add(currEntry);
                currEntry = "";
                currEntryContainedQuote = false;

                // Line ended
                ProcessLineFromCSV(currLineEntries, currLineIndex);
                currLineIndex++;
                currLineEntries = new List<string>();

                linesSinceUpdate++;
                if (linesSinceUpdate > kLinesBetweenUpdate)
                {
                    linesSinceUpdate = 0;
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                if (data[currCharIndex] == '"')
                {
                    inQuote = !inQuote;
                    currEntryContainedQuote = true;
                }

                // Entry level stuff
                {
                    if (data[currCharIndex] == ',')
                    {
                        if (inQuote)
                        {
                            currEntry += data[currCharIndex];
                        }
                        else
                        {
                            // If we were in a quote, trim bordering quotation marks
                            if (currEntryContainedQuote)
                            {
                                currEntry = currEntry.Substring(1, currEntry.Length - 2);
                            }

                            currLineEntries.Add(currEntry);
                            currEntry = "";
                            currEntryContainedQuote = false;
                        }
                    }
                    else
                    {
                        currEntry += data[currCharIndex];
                    }
                }
                currCharIndex++;
            }

            //progress = (int)((float)currCharIndex / data.Length * 100.0f);
        }

        onCompleted(null);
    }

    private void ProcessLineFromCSV(List<string> currentLineEntries, int currentLineIndex)
    {
        // The first line contains the column headers
        if (currentLineIndex == 0)
        {
            for (int columnIndex = 0; columnIndex < currentLineEntries.Count; columnIndex++)
            {
                //Debug.Log("Columns found: " + currentLineEntries[columnIndex]);
            }
        }
        else if (currentLineEntries.Count > 1)
        {
            downloadedEntries.Add(new DataTestHandler(currentLineEntries[1], currentLineEntries[2], currentLineEntries[3], currentLineEntries[4], currentLineEntries[5]));
        }
        else
        {
            Debug.LogError("Database line did not fall into one of the expected categories.");
        }
    }
}
