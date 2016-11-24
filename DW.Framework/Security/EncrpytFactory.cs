namespace DW.Framework.Security
{
    using System;

    public class EncrpytFactory
    {
        public static Rijndael_ CreateInstance(string key)
        {
            return new Rijndael_(key);
        }
    }
}

