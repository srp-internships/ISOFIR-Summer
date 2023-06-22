﻿namespace TestNinja.Fundamentals;

public class PhoneNumber
{
    private PhoneNumber(string area, string major, string minor)
    {
        Area = area;
        Major = major;
        Minor = minor;
    }

    public string Area { get; }
    public string Major { get; }
    public string Minor { get; }

    public static PhoneNumber Parse(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone number cannot be blank.");

        if (number.Length != 10)
            throw new ArgumentException("Phone number should be 10 digits long.");

        var area = number[..3];
        var major = number.Substring(3, 3);
        var minor = number[6..];

        return new PhoneNumber(area, major, minor);
    }

    public override string ToString()
    {
        return $"({Area}){Major}-{Minor}";
    }
}