namespace ExtensionMethods
{

    public static class MyExtension
    {
        public static List<string> ParseName(string s)
        {
            string[] lst = s.Split(new char[] { '.', ',', ' ', '(', ')', '-', '+', '^', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> ans = new List<string>();
            foreach (var str in lst)
            {
                if (str[0] >= 'A' && str[0] <= 'Z' && str[str.Length - 1] >= '0' && str[str.Length - 1] <= '9')
                {
                    ans.Add(str);
                }
            }
            return ans;
        }

        public static Tuple<int, int> NameToCoordinates(string s)
        {
            int i = 0, x = 0, y = 0;
            while (s[i] >= 'A' && s[i] <= 'Z')
            {
                x = x * 26 + Convert.ToInt32(s[i] - 65 + 1);
                i++;
            }
            int j = i;
            while (i < s.Length && s[i] >= '0' && s[i] <= '9')
            {
                y = y * 10 + Convert.ToInt32(s[i] - 48);
                i++;
            }
            return new Tuple<int, int>(x, y);
        }
        public static int Convert26To10(string s)
        {
            int i = 0, x = 0;
            while (i < s.Length && s[i] >= 'A' && s[i] <= 'Z')
            {
                x = x * 26 + Convert.ToInt32(s[i] - 65 + 1);
                i++;
            }
            if (i != s.Length)
            {
                throw new ArgumentException("Invalid argument");
            }
            return x;
        }

    }
}