using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Oyun sahnesini başlatma
    public void Oyna()
    {
        SceneManager.LoadScene("Oyun"); // Oyun sahnesinin ismini yazın
    }

    // Ayarlar sahnesine geçiş yapma
    public void Ayarlar()
    {
        SceneManager.LoadScene("Ayarlar"); // Ayarlar sahnesinin ismini yazın
    }

    // Krediler sahnesine geçiş yapma
    public void Credits()
    {
        SceneManager.LoadScene("Credits"); // Krediler sahnesinin ismini yazın
    }

    // Oyunu kapatma
    public void Cıkıs()
    {
        Application.Quit(); // Oyunu kapatır
    }
    
     // Ana menüye dönme
    public void MenuyeDon()
    {
        SceneManager.LoadScene("Ana Menü"); // Ana menü sahnesinin ismini yazın
    }

    




}

