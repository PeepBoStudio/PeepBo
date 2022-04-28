using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class Test : MonoBehaviour
{
    async void Start()
    {
        await RuntimeInitializer.InitializeAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
