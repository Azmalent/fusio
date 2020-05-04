using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingTextTrigger : MonoBehaviour
{
    public FadingText Target;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") Target.FadeIn();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") Target.FadeOut();
    }
}
