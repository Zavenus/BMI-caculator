using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMI
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 預設為圖形介面 (GUI)。若要以命令列模式執行，傳入參數 "console" 或 "-console"。
            bool consoleMode = args != null && args.Length > 0 && args.Any(a => a.Equals("console", StringComparison.OrdinalIgnoreCase) || a.Equals("-console", StringComparison.OrdinalIgnoreCase));

            if (consoleMode)
            {
                AllocConsole();
                RunConsoleMode();
                FreeConsole();
                return;
            }

            Application.Run(new Form1());
        }

        private static void RunConsoleMode()
        {
            Console.WriteLine("BMI 命令列模式 — 輸入 q 離開");
            while (true)
            {
                Console.Write("請輸入體重 (kg): ");
                var w = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(w)) continue;
                if (w.Trim().Equals("q", StringComparison.OrdinalIgnoreCase)) break;
                if (!double.TryParse(w, out double weightKg) || weightKg <= 0)
                {
                    Console.WriteLine("輸入錯誤：請輸入有效的體重數字");
                    continue;
                }

                Console.Write("請輸入身高 (cm): ");
                var h = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(h)) continue;
                if (h.Trim().Equals("q", StringComparison.OrdinalIgnoreCase)) break;
                if (!double.TryParse(h, out double heightCm) || heightCm <= 0)
                {
                    Console.WriteLine("輸入錯誤：請輸入有效的身高數字");
                    continue;
                }

                double heightM = heightCm / 100.0;
                double bmi = weightKg / (heightM * heightM);

                string category;
                ConsoleColor color;
                if (bmi < 18.5)
                {
                    category = "體重過輕";
                    color = ConsoleColor.Blue;
                }
                else if (bmi < 25)
                {
                    category = "正常範圍";
                    color = ConsoleColor.Green;
                }
                else if (bmi < 30)
                {
                    category = "過重";
                    color = ConsoleColor.Yellow;
                }
                else
                {
                    category = "肥胖";
                    color = ConsoleColor.Red;
                }

                var prev = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine($"BMI: {bmi:F2} — {category}");
                Console.ForegroundColor = prev;
                Console.WriteLine();
            }

            Console.WriteLine("按任意鍵結束...");
            Console.ReadKey(true);
        }
    }
}
