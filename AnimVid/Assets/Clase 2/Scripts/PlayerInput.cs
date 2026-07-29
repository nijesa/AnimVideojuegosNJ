using UnityEngine;

namespace AV2.Scripts
{
    public class PlayerInput : ICharacterInput
    {
        public float GetSpeedInput()
        {
            return new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            ).magnitude;
        }
    }
}
