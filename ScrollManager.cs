using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    public ScrollRect scrollRect; // ScrollRect bileşeni
    public Text historyText; // Gösterilecek metin
    public string[] historyItems; // Gösterilecek geçmiş öğeleri

    void Start()
    {
        UpdateHistoryText();
    }

    void UpdateHistoryText()
    {
        // Geçmiş metnini oluştur
        string combinedText = string.Join("\n", historyItems);
        historyText.text = combinedText;

        // ScrollRect içeriğini en alta kaydır
        Canvas.ForceUpdateCanvases(); // Canvas'ı güncelle
        scrollRect.verticalNormalizedPosition = 0; // En alta kaydır
    }
}

