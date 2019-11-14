using System;
using System.ComponentModel.DataAnnotations;

namespace Aviato.Attributes
{
    public class JMBGAttribute : ValidationAttribute
    {
        public override bool IsValid(object JMBG)
        {
            try
            {
                long mb = Convert.ToInt64(JMBG);
            }
            catch
            {
                return false;
            }
            string JMBGs = Convert.ToString(JMBG);
            if (JMBGs.Length != 13)
            {
                return false;
            }


            //m = 11 − ((7 * (a + g) + 6 * (b + h) + 5 * (c + i) + 4 * (d + j) + 3 * (e + k) + 2 * (f + l)) mod 11)

            int[] p = new int[12];
            int p13 = 0;

            p[0] = (JMBGs[0] - '0') * 7;
            p[1] = (JMBGs[1] - '0') * 6;
            p[2] = (JMBGs[2] - '0') * 5;
            p[3] = (JMBGs[3] - '0') * 4;
            p[4] = (JMBGs[4] - '0') * 3;
            p[5] = (JMBGs[5] - '0') * 2;
            p[6] = (JMBGs[6] - '0') * 7;
            p[7] = (JMBGs[7] - '0') * 6;
            p[8] = (JMBGs[8] - '0') * 5;
            p[9] = (JMBGs[9] - '0') * 4;
            p[10] = (JMBGs[10] - '0') * 3;
            p[11] = (JMBGs[11] - '0') * 2;

            for (int i = 0; i < 12; i++)
                p13 += p[i];

            p13 = p13 % 11;
            p13 = 11 - p13;

            if (p13 == 11)
                p13 = 0;

            if ((p13 == 10) || (p13 != (int)JMBGs[12] - '0'))
                return false;
            else
                return true;
        }
    }
}