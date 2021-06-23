using System.Text.RegularExpressions;

namespace MarsRoverAPI.Extensions
{
    public static class StringExtensions
    {
        public static bool CommandValidator(this string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                return false;
            }
            else if (!Regex.IsMatch(command, "^([LRM]|[lrm])+$"))
            {
                return false;
            }
            return true;
        }
    }
}