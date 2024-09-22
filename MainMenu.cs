using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Oyun sahnesini başlatma
    public void Oyna()
    {
        SceneManager.LoadScene("Oyun"); // Oyun sahnesi
    }

    // Ayarlar sahnesine geçiş yapma
    public void Ayarlar()
    {
        SceneManager.LoadScene("Ayarlar"); // Ayarlar sahnesi
    }

    // Krediler sahnesine geçiş yapma
    public void Credits()
    {
        SceneManager.LoadScene("Credits"); // Krediler sahnesi
    }

    // Oyunu kapatma
    public void Cıkıs()
    {
        Application.Quit(); // Oyunu kapat
    }
    
     // Ana menüye dönme
    public void MenuyeDon()
    {
        SceneManager.LoadScene("Ana Menü"); // Ana menü sahnesi
    }

    




}

