using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public class Utils
{

    public static string password_to_sha1(string input_str)
    {
        byte[] sha1_bs = SHA1.HashData(Encoding.UTF8.GetBytes(input_str));
        return BitConverter.ToString(sha1_bs).Replace("-", string.Empty);
    }

    public static bool is_sha1_hash(string input_str)
    {
        return Regex.IsMatch(input_str, @"\A[a-fA-F0-9]{40}\z");
    }


    public static DateTime FromDateOnly(DateOnly dateonly)
    {
        return new DateTime(dateonly.Year, dateonly.Month, dateonly.Day);
    }

    public static DateOnly FromDateTime(DateTime datetime)
    {
        return new DateOnly(datetime.Year, datetime.Month, datetime.Day);
    }

}

public partial class User
{
    public override string ToString()
    {
        return $"User: {UserId} - {FullName} - Role: {Role}";
    }
}
