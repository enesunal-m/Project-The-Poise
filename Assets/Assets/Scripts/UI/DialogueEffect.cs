using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    
    void Update()
    {
        text.ForceMeshUpdate();
        var textInfo = text.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }
            
            var vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            for (int j = 0; j < 4; j++)
            {
                var orig = vertices[charInfo.vertexIndex + j];
                vertices[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time*1.4f + orig.x*0.01f)* 10f, 0);
            }
        }
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            text.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
