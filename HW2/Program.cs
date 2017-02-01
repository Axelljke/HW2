using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    class Program
    {
        class StringCalcul
        {
            char[] num = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
            char[] operatorsarr = new char[4] { '+', '-', '*', '/'};
            public string str;//выражение на входе
            public string strans = String.Empty;//результат сложения строк
            int[] arg = new int[2];//массив аргументов
            public int ans=0; //решение выражения
            int operator1=4;//флаг оператора(+,-,*,/) По умолчанию 4(значение отсутствия оператора)
            public int Calculation()
            {
                // инициализация флага ошибки
                bool error1;
                // инициализация флага работы со строками
                bool strop = false;
                // инициализация стринг переменной buffer, в которую будут записываться символы аргумента
                string buffer = String.Empty;
                // инициализация ID аргумента/строки
                int x = 0;
                // инициализация "указателя" на начало 2 строки. Если =0 значит начало 2 строки не найдено. По умолчанию оно не найдено.
                int str2start = 0;
                //Перебор символов в строке и сравнение с набором допустимых символов
                if (str[0] == '"') { strop = true; }
                for (int i=0; i < str.Length; i++)
                {
                    // "обнуление" флага ошибки. По умолчани ошибка есть. При нахождении совпадения с массивом допустимых символов флаг ошибки получает значение false"
                    error1 = true;
                    // проверка на работу с числом
                    if(strop == false)
                    {
                        // проверка на пробел
                        if (str[i] == ' ') { continue; }
                        // проверка текущего символа с массивом из символов чисел
                        for (int j = 0; j <= 9; j++)
                        {
                            if (str[i] == num[j])
                            {
                                error1 = false;
                                buffer += str[i];
                                break;
                            }
                        }
                        //Проверка на знак операции если такой не был найден, и проверяемый символ не является началом или концом строки
                        if ((i != 0) && (i != (str.Length - 1)) && (x == 0))
                        {
                            for (int k = 0; k <= 3; k++)
                            {
                                if (str[i] == operatorsarr[k])
                                {
                                    error1 = false;
                                    arg[x] = Convert.ToInt32(buffer);
                                    buffer = String.Empty;
                                    operator1 = k;
                                    x++;
                                    break;
                                }
                            }
                        }
                        //проверка на ошибку
                        if (error1 == true) { goto errorHappens; }
                        //Проверка на конец строки. В положительном случае присваевается значение второму аргументу
                        if (((i + 1) == str.Length) && (operator1 !=4))
                        {
                            arg[x] = Convert.ToInt32(buffer);
                        }
                    }
                    else
                    {
                        //пропускаем " которые стоят вначале первой строки.
                        if (i == 0) { i++; }
                        //if((str[i]=='+')&& (i == 0)) { goto errorHappens; }//проверка на
                        //проверка на номер строки, с которой работает метод
                        if (x == 0)
                        {
                            //проверка на закрывающие " первой строки
                            if (str[i]!='"')
                            {
                                buffer += str[i];
                            }
                            else
                            {
                                if (i == 0) { continue; }
                                if (i == 1) { goto errorHappens; }
                                strans = buffer;
                                buffer = String.Empty;
                                x++;
                            }
                        }
                        else
                        {

                            if ((str2start == 0)&&(str[i] == ' ')) { continue; }//проверка на пробел
                            if ((str2start == 0) && (str[i] == '+') && (operator1 == 4)) { operator1 = 0; continue; }// проверка на знак +
                            if ((str2start == 0) && (str[i] != '"') && (operator1 == 4)) { goto errorHappens; }// проверка на другие знаки вместо +
                            if ((str2start == 0) && (str[i] == '"') && (operator1 == 0))
                            {
                                str2start = i + 1;
                                i++;
                            }
                            if (str[i] != '"')
                            {
                                if(i == (str.Length - 1)) { goto errorHappens; }
                                buffer += str[i];
                            }
                            else
                            {
                                if (i == str2start) { goto errorHappens; }
                                if(i!=(str.Length-1)) { goto errorHappens; }
                                strans += buffer;
                                buffer = String.Empty;
                                goto printstr;
                            } 
                        }
                        

                    }
                }
                //Проверка оператора
                if(strop==false)
                {
                    switch (operator1)
                    {
                        case 0:
                            ans = arg[0] + arg[1];
                            break;
                        case 1:
                            ans = arg[0] - arg[1];
                            break;
                        case 2:
                            ans = arg[0] * arg[1];
                            break;
                        case 3:
                            ans = arg[0] / arg[1];
                            break;
                        default:
                            goto errorHappens;
                    }
                }
                Console.WriteLine("{0} {1} {2} = {3}",arg[0], operatorsarr[operator1], arg[1], ans);
                return ans;
            //указатель для перехода к выводу результата строкового значения
            printstr:
                Console.WriteLine("{0} = {1}", str, strans);
                return 0;
            //указатель для перехода при ошибке
            errorHappens:
                Console.WriteLine("\nОШИБКА: Не правильный формат введеного выражения\n");
                return 0;
            }
        }
        static void Main(string[] args)
        {
            while(true)
            {
                int answer;
                StringCalcul strcalcul1 = new StringCalcul();
                Console.WriteLine("\nВведите выражение для вычисления или exit для выхода\n");
                strcalcul1.str=Console.ReadLine();
                if (strcalcul1.str == "exit") { break; }
                answer=strcalcul1.Calculation();
                Console.WriteLine("Нажмите Enter чтобы продолжить");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
