using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataPanelMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI buffText;
    [SerializeField] private TextMeshProUGUI typeText;

    public TextMeshProUGUI NameText => nameText;
    public TextMeshProUGUI PriceText => priceText;
    public TextMeshProUGUI BuffText => buffText;
    public TextMeshProUGUI TypeText => typeText;
}
