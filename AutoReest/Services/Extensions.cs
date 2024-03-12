using AutoReest.Model;

namespace AutoReest.Services
{
    public static class Extensions
    {
        /// <summary>
        /// Преобразует строку в объект TableData
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TableData ToTableData(this string value, bool isCorrectPage)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var splitString = value.Split(' ');
            if (splitString[0] == null || splitString[0] == " " || !int.TryParse(splitString[0], out int result) || !isCorrectPage)
            {
                var table = new TableData()
                {
                    NumberOfColumn = null,
                    NumberOfDocument = null,
                    Date = null
                };
                return table;
            }
            else {
                var table = new TableData()
                {
                    NumberOfColumn = splitString[0],
                    NumberOfDocument = splitString[1],
                    Date = splitString[2]
                };

                return table;
            }
        }

        public static string[] ToStrings(this System.Text.RegularExpressions.MatchCollection collection)
        {
            var strings = new string[collection.Count];

            for (int i = 0; i < collection.Count; i++)
                strings[i] = collection[i].Value;

            return strings;
        }
    }
}

