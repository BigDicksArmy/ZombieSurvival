public static class ExtensionMethods
{
	public static string RemoveAfter(this string s, char sign)
	{
		int index;
		for (int i = 0; i < s.Length; i++)
		{
			if (s[i] == sign)
			{
				index = i;
				char[] newString = new char[index];
				
				for (int j = 0; j < index; j++)
				{
					newString[j] = s[j];
				}
				string tmp = new string(newString);
				return tmp;
			}
		}
		return "0x0";
	}
}
