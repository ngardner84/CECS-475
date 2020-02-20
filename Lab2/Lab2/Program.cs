using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*1.
000
001
010
011
100
101
110
 
2. 0111

3. 1000

4. 00001111

*/
namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            //5
            byte x = 0b_1101_0011;
            Console.WriteLine(x.ToString());
            Console.WriteLine(Convert.ToString(x, toBase: 2));
            byte y = (byte)(x >> 4);
            int z = Convert.ToInt32(Convert.ToString(y, 2).PadLeft(8, '0'));
            Console.WriteLine(z);

            //6
            byte color = 0b_0000_0001;
            byte piece = 0b_0000_0110;
            int colorInt = (int)(color);
            int pieceInt = (int)(piece);
            if (colorInt == 1)
            {
                pieceInt += 8;
            }
            byte result = (byte)(pieceInt);
            Console.WriteLine(Convert.ToString(result, 2));

            //7
            byte test = 0b_1011_0000;
            test = (byte)(test << 4);
            Console.WriteLine(Convert.ToString(test, 2));

            //8
            byte a = 0b_1101_0000;
            byte b = 0b_0000_0011;
            int c = (int)(a);
            int d = (int)(b);
            c += d;
            byte addedByte = (byte)(c);
            Console.WriteLine(Convert.ToString(addedByte, 2));
        }
    }
}
