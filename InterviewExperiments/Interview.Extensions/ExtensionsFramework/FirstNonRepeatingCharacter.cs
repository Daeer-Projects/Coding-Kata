using System.Collections.Concurrent;

namespace ExtensionsFramework
{
    public static class FirstNonRepeatingCharacter
    {
        public static char GetFirstNonRepeatedCharacter(this string paragraph)
        {
            var result = '_';

            if (!string.IsNullOrWhiteSpace(paragraph))
            {
                var characters = new ConcurrentDictionary<char, int>();

                foreach (var character in paragraph)
                {
                    characters.AddOrUpdate(character, 1,
                        (key, existingValue) => existingValue + 1);
                }

                foreach (var character in characters)
                {
                    if (character.Value == 1)
                    {
                        result = character.Key;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
