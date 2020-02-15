namespace TrackAChild.Helpers
{
    public static class StringHelper
    {
        public static string GetFormattedStringForHttpRequest(string inputName)
        {
            if (inputName.Length > 0)
            {
                var splitElement = inputName.Split(' ');
                var httpElement = "";
                for (int index = 0; index < splitElement.Length; ++index)
                {
                    if (index == splitElement.Length - 1)
                    {
                        httpElement += splitElement[index];
                    }
                    else
                    {
                        httpElement += splitElement[index] + "+";
                    }
                }

                return httpElement;
            }

            return "";
        }
    }
}
