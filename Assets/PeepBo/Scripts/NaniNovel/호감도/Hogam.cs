using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hogam : MonoBehaviour
{
    [SerializeField] Sprite 고결;
    [SerializeField] Sprite 유겸;
    [SerializeField] Sprite 우준;
    [SerializeField] Sprite 다함;

    Image image;
    int score;

    public void InitHogam(string name, int _score)
    {
        if (name == "고결")
            image.sprite = 고결;
        else if (name == "유겸")
            image.sprite = 유겸;
        else if (name == "우준")
            image.sprite = 우준;
        else if (name == "다함")
            image.sprite = 다함;

        var text = image.GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"호감도 + {_score}";

        score = _score;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        GameManager.Hogam.SetHogamScript(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
