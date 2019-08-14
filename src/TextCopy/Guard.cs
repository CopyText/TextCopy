using System;

static class Guard
{
    public static void AgainstNull(object value, string argumentName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(argumentName);
        }
    }
}