using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;


public class StoryManager : MonoBehaviour
{
    public Text storyText; // Hikaye metnini göstermek için UI Text.
    public Text hikayeText; // Geçmiş metnini gösterecek Text bileşeni

    public Button[] optionButtons; // Seçenekler için butonlar.
    public Button backButton; // Geri al butonu
    public Button historyButton; // Geçmiş butonu
    public RawImage storyImage; // Hikaye resmi için UI RawImage

    public StoryContainer StoryContainer;

    private StoryNode gecerliNode; // Şu anki hikaye düğümü.
    private List<StoryNode> history; // Geçmiş düğümler.
    private Dictionary<int, StoryNode> storyNodes; // Tüm düğümleri tutacak bir sözlük.

    private string jsonFilePath = "StreamingAssets/story.json"; // JSON dosyasının yolu.
    private StoryNode savedNode; // Kayıtlı düğüm



    void Start()
    {
        LoadStoryFromJSON(); // JSON'dan hikayeyi yükle.
        gecerliNode = storyNodes[0]; // Başlangıç düğümü
        history = new List<StoryNode>(); // Geçmişi tutmak için liste
        AktifNodeKontrol();
    }

    // JSON'dan hikaye düğümlerini yükleme.
    void LoadStoryFromJSON()
    {
        storyNodes = new Dictionary<int, StoryNode>();

        // JSON dosyasını oku
        
        // JSON dosyasının adı (StreamingAssets klasöründe yer alıyor)
        string dosyaAdi = "story.json";

        // Platforma göre dosya yolunu ayarla
        string dosyaYolu = Path.Combine(Application.streamingAssetsPath, dosyaAdi);

        // Dosyayı oku (Windows, macOS, Linux için doğrudan erişim)
        if (File.Exists(dosyaYolu))
        {
            string jsonIcerik = File.ReadAllText(dosyaYolu);
            Debug.Log("JSON içeriği: " + jsonIcerik);
        }
        else
        {
            Debug.LogError("Dosya bulunamadı: " + dosyaYolu);
        }

        string jsonText = File.ReadAllText(dosyaYolu);

        // JSON'u StoryContainer'e deserialize et
        StoryContainer storyData = JsonUtility.FromJson<StoryContainer>(jsonText);

        // StoryContainer'daki düğümleri sözlüğe ekle
        foreach (StoryNode node in storyData.storyNodes)
        {
            storyNodes.Add(node.id, node);
        }
    }

    // Mevcut düğümü gösterme.
    void AktifNodeKontrol()
    {
        // Hikaye metnini güncelle
        storyText.text = gecerliNode.text;

        if (!string.IsNullOrEmpty(gecerliNode.imagePath))
        {
            Texture2D texture = Resources.Load<Texture2D>(gecerliNode.imagePath); // 'Images/image' 
            if (texture != null)
            {
                storyImage.texture = texture; // RawImage'ye atama
                storyImage.gameObject.SetActive(true); // Resmi görünür yap
            }
            else
            {
                Debug.LogError("Resim yüklenemedi: " + gecerliNode.imagePath);
                storyImage.gameObject.SetActive(false); // Resim bulunamazsa gizle
            }
        }

        else
        {
            storyImage.gameObject.SetActive(false); // Resim yolu boşsa gizle
        }

   
            // // Resmi yükleme ve UI bileşenine atama
            // Texture2D texture = Resources.Load<Texture2D>(gecerliNode.imagePath.Replace("Assets/Resources/", "").Replace(".png", ""));
       
       

        // Geçmişi güncelle
        if (!history.Contains(gecerliNode))
        {
            history.Add(gecerliNode);
            UpdateHistoryDisplay(); // Geçmişi güncelle
        }

        // Seçenek butonlarını güncelle
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < gecerliNode.options.Count)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<Text>().text = gecerliNode.options[i].text;
                int choiceIndex = i; // Closure sorunu çözmek için index'i yakala
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(choiceIndex));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false); // Bu düğümde yeterince seçenek yoksa butonları gizle
            }
        }

        // Back butonunu görünür yap
        backButton.gameObject.SetActive(history.Count > 1);
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(BackToPreviousNode);

        // History butonunu görünür yap
        historyButton.gameObject.SetActive(history.Count > 1); // Geçmişte en az 2 düğüm varsa görünür olsun
        historyButton.onClick.RemoveAllListeners();
        historyButton.onClick.AddListener(ShowHistory);
    }

    // Seçenek seçildiğinde çağrılır.
    void OnOptionSelected(int index)
    {
        int sonrakiNodeId = gecerliNode.options[index].sonrakiNodeId;

        // Seçilen düğümün options listesini kontrol et
        if (storyNodes.ContainsKey(sonrakiNodeId))
        {
            gecerliNode = storyNodes[sonrakiNodeId];

            // Eğer bu son düğümse, metni göster ve butonları güncelle
            if (!gecerliNode.options.Any())
            {
                storyText.text = gecerliNode.text; // Son düğüm metnini göster
                OyunSonu(); // Butonları güncelle
            }
            else
            {
                AktifNodeKontrol(); // Eğer son düğüm değilse mevcut düğümü göster
            }
        }
    }



    void OyunSonu()
    {
        // İlk buton: Yeniden Oyna
        optionButtons[0].gameObject.SetActive(true);
        optionButtons[0].GetComponentInChildren<Text>().text = "Yeniden Oyna";
        optionButtons[0].onClick.RemoveAllListeners();
        optionButtons[0].onClick.AddListener(() => RestartGame());

        // İkinci buton: Yapımcı
        optionButtons[1].gameObject.SetActive(false);


    }

    void RestartGame()
    {
        gecerliNode = storyNodes[0]; // Başlangıç düğümüne geri dön
        history.Clear(); // Geçmişi temizle
        AktifNodeKontrol(); // Mevcut düğümü göster...
    }





    // Geri alma işlevi
    void BackToPreviousNode()
    {
        if (history.Count > 1)
        {
            history.RemoveAt(history.Count - 1); // Son metni sil
            gecerliNode = history[history.Count - 1]; // Sonraki metin
            AktifNodeKontrol();
        }
    }

    void ShowHistory()
    {
        savedNode = gecerliNode; // Geçerli düğümü kaydet
        SceneManager.LoadScene("Hikaye"); // Geçmiş sahnesine geç
    }

    void UpdateHistoryDisplay()
    {
        hikayeText.text = ""; // Önceki metni temizle
        foreach (StoryNode node in history)
        {
            hikayeText.text += node.text + "\n"; // Geçmiş metinleri ekle
        }
    }


    public void ReturnToGame()
    {
        SceneManager.LoadScene("Oyun"); // Oyun sahnesine geç
                                        // Burada kaydedilen düğümü kullanarak durumu güncelleyin
        StoryManager storyManager = FindObjectOfType<StoryManager>();
        if (storyManager != null)
        {
            storyManager.ResumeGame(savedNode); // Kayıtlı düğümü geri yükle
        }
    }

    public void ResumeGame(StoryNode node)
    {
        if (node != null)
        {
            gecerliNode = node; // Kayıtlı düğüme geri dön
        }

        AktifNodeKontrol(); // Durumu güncelle
    }


}

