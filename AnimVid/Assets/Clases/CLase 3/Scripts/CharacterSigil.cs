using UnityEngine;

public class CharacterSigil : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private Material material;
    private Color originalColor;
    [SerializeField] private Color sigilColor;
    [SerializeField] private float colorChangeSpeed;
    public Character ParentCharacter { get; set; }

    void Awake()
    {
        originalColor = material.color;
    }
    // Update is called once per frame
    void Update()
    {
        if(ParentCharacter == null) return;
        if(ParentCharacter.IsCrouching)
        {
            if(material.color == sigilColor) return;
            material.color = Color.Lerp(material.color, sigilColor, colorChangeSpeed * Time.deltaTime);
        }
        else
        {
            if(material.color == originalColor) return;
            material.color = Color.Lerp(material.color, originalColor, colorChangeSpeed * Time.deltaTime);
        }
    }

   
}
