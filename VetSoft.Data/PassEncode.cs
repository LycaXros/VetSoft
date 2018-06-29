using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VetSoft.Data
{
    class PassEncode
    {
    }
    public static class Encriptador
    {
        public static readonly string KEY = "AbcD3fGhiJk1MnoQrs7uvWx0";
        //https://stackoverflow.com/questions/11413576/how-to-implement-triple-des-in-c-sharp-complete-example

        /// <summary>
        /// Encriptacion en Triple Des o 3DES
        /// </summary>
        /// <param name="strEncriptar"></param>
        /// <param name="bytPK"></param>
        /// <returns></returns>
        public static string Encriptar(string strEncriptar)
        {

            TripleDESCryptoServiceProvider Tdes = new TripleDESCryptoServiceProvider();
            byte[] encrypted = null;
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(KEY);
            try
            {
                Tdes.Key = keyArray;
                Tdes.Mode = CipherMode.ECB;
                Tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform crypT = Tdes.CreateEncryptor();
                byte[] toEncrypt = UTF8Encoding.UTF8.GetBytes(strEncriptar);
                encrypted = crypT.TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { Tdes.Clear(); }

            return Convert.ToBase64String(encrypted, 0, encrypted.Length);
        }

        public static string Desencriptar(string bytDesEncriptar)
        {

            byte[] toEncrypt = Convert.FromBase64String(bytDesEncriptar);
            byte[] resultArray = { };
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(KEY);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            try
            {
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                //mode of operation. there are other 4 modes. 
                //We choose ECB(Electronic code Book)

                tdes.Mode = CipherMode.ECB;
                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                resultArray = cTransform.TransformFinalBlock(
                                    toEncrypt, 0, toEncrypt.Length);


            }
            catch { }
            finally { tdes.Clear(); }
            //Release resources held by TripleDes Encryptor                

            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

    }
}
