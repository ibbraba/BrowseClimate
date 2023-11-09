﻿using System.Diagnostics;
using System.Security.Cryptography;

namespace BrowseClimate.Services.UserServices
{
    public class HashPassword
    {


        public string Generate(string password, int iterations = 1000)
        {
            //generate a random salt for hashing
            var salt = new byte[24];
            new RNGCryptoServiceProvider().GetBytes(salt);

            //hash password given salt and iterations (default to 1000)
            //iterations provide difficulty when cracking
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(24);

            //return delimited string with salt | #iterations | hash
            return Convert.ToBase64String(salt) + "|" + iterations + "|" +
                Convert.ToBase64String(hash);

        }

        public bool IsValid(string testPassword, string origDelimHash)
        {
            //extract original values from delimited hash text
            var origHashedParts = origDelimHash.Split('|');
            var origSalt = Convert.FromBase64String(origHashedParts[0]);
            var origIterations = Int32.Parse(origHashedParts[1]);
            var origHash = origHashedParts[2];

            //generate hash from test password and original salt and iterations
            var pbkdf2 = new Rfc2898DeriveBytes(testPassword, origSalt, origIterations);
            byte[] testHash = pbkdf2.GetBytes(24);
            string newHash = Convert.ToBase64String(testHash);

            Debug.WriteLine("OrigHash :" + origHash);
            Debug.WriteLine("NewHash :" + newHash);

            //if hash values match then return success
            if (newHash.Trim() == origHash.Trim())
            {
                return true;
            }
            else
            {

                return false;
            };

            //no match return false

        }


    }
}
