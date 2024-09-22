using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryOption
{
    public string text; // Seçenek metni
    public int sonrakiNodeId; // Bu seçenek seçildiğinde gidilecek düğümün ID'si
}

[System.Serializable]
public class StoryNode
{
    public int id; // Düğümün benzersiz ID'si
    public string text; // Bu düğümde gösterilecek metin
    public List<StoryOption> options; // Seçenekler listesi
    public string imagePath; // Bu düğümde gösterilecek resim yolu

}

[System.Serializable]
public class StoryContainer
{
    public List<StoryNode> storyNodes; // Hikayedeki tüm düğümler
}
