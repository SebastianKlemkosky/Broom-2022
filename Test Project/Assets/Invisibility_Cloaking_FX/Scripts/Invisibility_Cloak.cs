using UnityEngine;
using System.Collections;

public class Invisibility_Cloak : MonoBehaviour {
 
public GameObject characterGeometry;
public GameObject characterCloaked; 
public Material cloakMaterial;
public AudioSource cloakAudio;

private Color cloakColor = Color.white;
private bool cloaked = false; 
private bool cloakChange = true; 


void Start (){

    characterGeometry.SetActive(true);
    characterCloaked.SetActive(false);

}

 
void Update (){

    if (Input.GetButtonDown("Fire1"))
    {

        cloakChange = false;     

        if (cloaked == false)
        {
			StartCoroutine("CloakCharacter");
            cloaked = true;
            cloakChange = true;
        }


        if (cloaked == true)
        {
            if (cloakChange == false)
            {
				StartCoroutine("DeCloakCharacter");
                cloaked = false;             
            }
        }
    
   
    }

}
     

IEnumerator CloakCharacter (){

    cloakAudio.Play();

    characterGeometry.SetActive(false);
    characterCloaked.SetActive(true);

    float cloakFade = 1.0f;
    cloakColor.r = cloakFade;
    cloakColor.g = cloakFade;
    cloakColor.b = cloakFade;
    cloakMaterial.SetColor("_Tint", cloakColor); 

    while (cloakFade >= 0.5f)
    {

        cloakFade -= 0.015f;
        cloakColor.r = cloakFade;
        cloakColor.g = cloakFade;
        cloakColor.b = cloakFade;
        cloakMaterial.SetColor("_Tint", cloakColor);
        yield return 0;

    }

}


IEnumerator DeCloakCharacter (){

    cloakAudio.Play();

    float cloakFade = 0.5f;
    cloakColor.r = cloakFade;
    cloakColor.g = cloakFade;
    cloakColor.b = cloakFade;
    cloakMaterial.SetColor("_Tint", cloakColor); 

    while (cloakFade <= 1.0f)
    {

        cloakFade += 0.015f; 
        cloakColor.r = cloakFade;
        cloakColor.g = cloakFade;
        cloakColor.b = cloakFade;
        cloakMaterial.SetColor("_Tint", cloakColor);
        yield return 0;

        if (cloakFade >= 1.0f)
        {
            characterGeometry.SetActive(true);
            characterCloaked.SetActive(false);
        }

    }

}



}