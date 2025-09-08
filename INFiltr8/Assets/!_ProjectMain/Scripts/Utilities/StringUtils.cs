using System;

namespace __ProjectMain.Scripts.Utilities
{
    public class StringUtils
    {
        // Simple Levenshtein distance
        public static int LevenshteinDistance(string a, string b)
        {
            if (string.IsNullOrEmpty(a)) return string.IsNullOrEmpty(b) ? 0 : b.Length;
            if (string.IsNullOrEmpty(b)) return a.Length;

            int[,] d = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= b.Length; j++) d[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = (char.ToLower(a[i - 1]) == char.ToLower(b[j - 1])) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[a.Length, b.Length];
        }

        public static string ToMinSecMilli(double time)
        {
            return string.Format("{0}:{1:00}.{2:000}", (int)time / 60, (int)(time % 60), (time - (int)time) * 1000);
        }
    }
}