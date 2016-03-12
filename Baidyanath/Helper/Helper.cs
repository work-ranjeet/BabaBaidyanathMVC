using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baidyanath.Helper
{
    public class Helpers
    {
        public static string CommaSeperatedNumber(string Number)
        {
            string[] strArr = Number.Split('.');
            int length = strArr[0].Length;
            string strAmt = "";

            switch (length)
            {
                case 4:
                    strAmt = strArr[0].ToString().Substring(0, 1) + "," + strArr[0].ToString().Substring(1, 3);
                    break;

                case 5:
                    strAmt = strArr[0].ToString().Substring(0, 2) + "," + strArr[0].ToString().Substring(2, 3);
                    break;

                case 6:
                    strAmt = strArr[0].ToString().Substring(0, 1) + "," + strArr[0].ToString().Substring(1, 2) + "," + strArr[0].ToString().Substring(3, 3);
                    break;

                case 7:
                    strAmt = strArr[0].ToString().Substring(0, 1) + "," + strArr[0].ToString().Substring(1, 3) + "," + strArr[0].ToString().Substring(4, 3);
                    break;

                case 8:
                    strAmt = strArr[0].ToString().Substring(0, 2) + "," + strArr[0].ToString().Substring(2, 3) + "," + strArr[0].ToString().Substring(5, 3);
                    break;

                case 9:
                    strAmt = strArr[0].ToString().Substring(0, 3) + "," + strArr[0].ToString().Substring(3, 3) + "," + strArr[0].ToString().Substring(6, 3);
                    break;

                case 10:
                    strAmt = strArr[0].ToString().Substring(0, 4) + "," + strArr[0].ToString().Substring(4, 3) + "," + strArr[0].ToString().Substring(7, 3);
                    break;

                default:
                    strAmt = strArr[0];
                    break;

            }
            return strAmt;
        }
    }
}