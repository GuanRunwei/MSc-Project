using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pathological_section_diagnosis_assistant_software
{
    class Encrption
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns></returns>
        public static string MD5Hash(string plaintext)
        {
            MD5 md5 = MD5.Create();
            string p = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(plaintext.Trim()))).Replace("-", "");
            md5.Clear();
            md5.Dispose();
            return p;
        }
    }
}
