using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDisplay = null;
    [SerializeField] TextMeshProUGUI continueDisplay = null;
    [SerializeField] string[] sentences = null;
    private int index;
    [SerializeField] float typingSpeed = 0.04f;
    //[SerializeField] float fadeSpeed = 0.01f;
    private bool continueText;
    private bool canContinue = false;
    [SerializeField] Image textBox = null;
    [SerializeField] [Range(0f,255f)] float imageAlpha = 0.5f;

    public void Start()
    {
        // initalize text boxes
        continueDisplay.text = "F to Continue";
        continueDisplay.alpha = 0f;
        textDisplay.text = "";

        // alpha of the black text box
        Color tempColor = textBox.color;
        tempColor.a = imageAlpha/255f;
        textBox.color = tempColor;

        // start whatever is in the text messages
        StartCoroutine(Type());
    }

    public void Update()
    {
        // executes next message in the list
        NextSentence();
    }

    private void NextSentence()
    {
        // press f to continue
        continueText = Input.GetKeyDown(KeyCode.F);

        if (continueText == false || !canContinue) return;

        // if there are still messages, type it
        if (index < sentences.Length - 1 )
        {
            canContinue = false;
            index++;
            textDisplay.text = "";
            continueDisplay.alpha = 0f;
            StartCoroutine(Type());
        }
        else
        {
            // if there are no messages to type
            continueDisplay.text = "";
            textDisplay.text = "";

            // black text box disappears
            Color tempColor = textBox.color;
            tempColor.a = 0f;
            textBox.color = tempColor;
        }
    }
    IEnumerator Type()
    {
        // for each letter in the sentence, put it in the message, wait typingSpeed seconds and
        // do that again
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // you can continue to the next text in the list
        canContinue = true;
        continueDisplay.alpha = 1f;
        //FadeText(continueDisplay, 1f);
    }

    /*
    IEnumerator FadeText(TextMeshProUGUI text, float alpha)
    {
        while (text.color.a != alpha)
        {
            Color txtColor = text.color;
            float diff = Mathf.Sign(txtColor.a - alpha);
            txtColor.a = txtColor.a - diff;
            text.color = txtColor;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
    */
}
