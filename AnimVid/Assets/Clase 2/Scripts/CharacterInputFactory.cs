using System;

namespace AV2.Scripts
{
    public static class CharacterInputFactory
    {
        public static ICharacterInput CreateInput(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Player:
                    return new PlayerInput();
                    break;
                case InputType.Enemy:
                    return new EnemyInput();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
            }
        }
        public enum InputType
        {
            Player,
            Enemy
        }
    }
}