using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake_Script : MonoBehaviour
{
   public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 original_Camera_Pos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, original_Camera_Pos.z);


            elapsed += Time.deltaTime;

            yield return null;
        }



        transform.localPosition = original_Camera_Pos;

    }



}
