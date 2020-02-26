using System.Collections.Concurrent;

namespace Extensions
{
    public static class FirstNonRepeatedCharacter
    {
        public static char GetFirstNonRepeatedCharacter(this string paragraph)
        {
            var result = '_';

            if (!string.IsNullOrWhiteSpace(paragraph))
            {
                var characters = new ConcurrentDictionary<char, int>();

                for (var i = 0; i < paragraph.Length; i++)
                {
                    characters.AddOrUpdate(paragraph[i], 1,
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
