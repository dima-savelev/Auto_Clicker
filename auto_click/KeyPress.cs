using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace auto_click
{
    static class KeyPress
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern short GetAsyncKeyState(int vkey);

        //Перечесление клавиш
        public enum Key { Ctrl, F6 };
        //Делегат события
        public delegate void keyPress(Key Key);
        //Событие
        public static event keyPress OnKeyPressed;
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        static CancellationToken token = cancellationTokenSource.Token;
        //Отдельный поток для отлавливания клавиш
        static Task th = new Task(() =>
        {
            while (true)
            {
                //Коды клавиш
                //Вверх - 0x26
                //Вниз - 40
                //Вправо - 0x27
                //Влево - 0x25
                if (OnKeyPressed != null)
                {
                    if (GetAsyncKeyState(0x11) != 0)
                        OnKeyPressed(Key.Ctrl);

                    if (GetAsyncKeyState(0x75) != 0)
                        OnKeyPressed(Key.F6);
                }

                Thread.Sleep(100);
            }
        });
        //Старт доп потока
        public static void Start()
        {
            th.Start();
        }
        //Остановка доп потока
        public static void Stop()
        {
            cancellationTokenSource.Dispose();
        }
    }
}
