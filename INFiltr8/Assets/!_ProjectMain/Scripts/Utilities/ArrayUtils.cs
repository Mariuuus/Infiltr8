using System;
using System.Linq;

namespace __ProjectMain.Scripts.Utilities
{
    public class ArrayUtils
    {
        public static T[] ShuffleArray<T>(T[] array)
        {
            System.Random random = new System.Random();
            return array.OrderBy(x => random.Next()).ToArray();
        }
    }
}