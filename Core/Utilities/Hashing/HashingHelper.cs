using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Hashing
{
    public class HashingHelper
    {
        public static void CreateContentHash(byte[] filecontent, out byte[] fileContentHash, out byte[] fileContentSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256()) 
            {
                fileContentSalt = hmac.Key;
                fileContentHash = hmac.ComputeHash(filecontent);
            }
        }
        public static bool VerifyFileContentHash(byte[] filecontent, byte[] fileContentHash, byte[] fileContentSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(fileContentSalt)) 
            {
                var computedHash = hmac.ComputeHash(filecontent);
                for (int i = 0; i < computedHash.Length; i++) 
                {
                    if (computedHash[i]!=fileContentHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}