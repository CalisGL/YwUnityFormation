using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderoutline : MonoBehaviour
{
    // Reference to the SkinnedMeshRenderer component of the object
    private SkinnedMeshRenderer skinnedMeshRenderer;

    // Reference to the MeshRenderer component of the object
    private MeshRenderer meshRenderer;

    // Material for the outline effect
    public Material outlineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        // Attempt to get the SkinnedMeshRenderer component attached to this GameObject
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        // If SkinnedMeshRenderer is not found, try to get the MeshRenderer component
        if (skinnedMeshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        // Check if either SkinnedMeshRenderer or MeshRenderer component exists
        if (skinnedMeshRenderer != null || meshRenderer != null)
        {
            // Determine which renderer is found and assign the outline material accordingly
            if (skinnedMeshRenderer != null && PlayerPrefs.GetInt("outline", 1) == 1)
            {
                AddOutlineMaterial(skinnedMeshRenderer);
            }
            else if(PlayerPrefs.GetInt("outline", 1) == 1)
            {
                AddOutlineMaterial(meshRenderer);
            }
        }
        else
        {
            // Log an error if neither SkinnedMeshRenderer nor MeshRenderer component is found
            Debug.LogError("SkinnedMeshRenderer or MeshRenderer component not found!");
        }
    }

    // Method to add the outline material to the SkinnedMeshRenderer
    private void AddOutlineMaterial(SkinnedMeshRenderer renderer)
    {
        // Get the current materials array
        Material[] materials = renderer.materials;

        // Create a new array with space for an additional material
        Material[] newMaterials = new Material[materials.Length + 1];

        // Copy the existing materials into the new array
        for (int i = 0; i < materials.Length; i++)
        {
            newMaterials[i] = materials[i];
        }

        // Add the outline material to the end of the new array
        newMaterials[newMaterials.Length - 1] = outlineMaterial;

        // Assign the new materials array to the SkinnedMeshRenderer
        renderer.materials = newMaterials;
    }

    // Method to add the outline material to the MeshRenderer
    private void AddOutlineMaterial(MeshRenderer renderer)
    {
        // Create a new materials array with a length of 1
        Material[] materials = new Material[1];

        // Add the outline material to the array
        materials[0] = outlineMaterial;

        // Assign the new materials array to the MeshRenderer
        renderer.materials = materials;
    }

    // Update is called once per frame
    void Update()
    {
        // You can add update logic here if needed
    }
}