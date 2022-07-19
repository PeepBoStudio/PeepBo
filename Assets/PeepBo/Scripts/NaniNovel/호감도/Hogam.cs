using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hogam : MonoBehaviour
{
    [SerializeField] Sprite ���;
    [SerializeField] Sprite ����;
    [SerializeField] Sprite ����;
    [SerializeField] Sprite ����;

    Image image;
    int score;

    public void InitHogam(string name, int _score)
    {
        if (name == "���")
            image.sprite = ���;
        else if (name == "����")
            image.sprite = ����;
        else if (name == "����")
            image.sprite = ����;
        else if (name == "����")
            image.sprite = ����;

        var text = image.GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"ȣ���� + {_score}";

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
