using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenEmail : MonoBehaviour
{
    string email = "";
    string subject = "Tattoo Request";
    string body;
    public TMP_InputField name;
    public TMP_InputField bday;
    public TMP_InputField phoneNo;
    public TMP_InputField motive;
    public TMP_InputField motiveSize;
    public TMP_InputField placement;
    public TMP_InputField date;

    public void openMail(){

        body = "Hallo" + "\n" + "\n" + 
        "ich bin ein großer Fan deiner Arbeiten und würde gerne ein Tattoo von dir machen lassen. " +
        "Hast du aktuell Termine frei? Ich würde mich sehr freuen, wenn wir einen Termin vereinbaren könnten." + "\n" + "\n" + 
        "Name: " + name.text + "\n" + 
        "Geburtsdatum: " + bday.text + "\n" + 
        "Handynummer: " + phoneNo.text + "\n" + 
        "Motiv: " + motive.text + "\n" + 
        "Größe des Motivs: " + motiveSize.text + "\n" + 
        "Platzierung: " + placement.text + "\n" + 
        "Mögliches Datum/Wochentage: " + date.text + "\n" + "\n" + 
        "Danke und liebe Grüße" + name.text;
        Debug.Log("OpenMail success: " + body);
        Debug.Log("OpenMail Name: " + name.text);
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
}
