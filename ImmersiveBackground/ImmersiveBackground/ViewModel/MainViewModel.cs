using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Forms;
using ImmersiveBackground.Model;

namespace ImmersiveBackground.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public static string file;
        public static Uri fileMedia;
        public static Window win2;
        public static MediaElement media = null;

        public static List<Window> windowList;
        public static List<System.Drawing.Rectangle> ScreenList;
        public static List<System.Windows.Controls.Button> ButtonList;

        public static IntPtr workerw;
        public static IntPtr workerwHidden;

        public static MediaPlayer player;

        public static bool soundEnabled;
        public static bool isPlaying;

        public MainViewModel()
        {
            media = null;
            soundEnabled = false;
            isPlaying = false;

            player = new MediaPlayer { Volume = 0, ScrubbingEnabled = true };

            windowList = new List<Window>();
            ScreenList = new List<System.Drawing.Rectangle>();
            ButtonList = new List<System.Windows.Controls.Button>();
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                ScreenList.Add(Screen.AllScreens[i].WorkingArea);
            }
            for (int i = 0; i < ScreenList.Count; i++)
            {
                windowList.Add(new Window());
            }

            for (int i = 0; i < ScreenList.Count; i++)
            {
                ButtonList.Add(new System.Windows.Controls.Button());
                ButtonList[i].Name = "Screen" + i.ToString();
                ButtonList[i].Content = "Screen " + i.ToString();
            }
        }        

        public static void findWorker()
        {
            IntPtr progman = W32.FindWindow("Progman", null);

            IntPtr result = IntPtr.Zero;

            W32.SendMessageTimeout(progman,
                                   0x052C,
                                   new IntPtr(0),
                                   IntPtr.Zero,
                                   W32.SendMessageTimeoutFlags.SMTO_NORMAL,
                                   1000,
                                   out result);

            workerw = IntPtr.Zero;

            W32.EnumWindows(new W32.EnumWindowsProc((tophandle, topparamhandle) =>
            {
                IntPtr p = W32.FindWindowEx(tophandle,
                                            IntPtr.Zero,
                                            "SHELLDLL_DefView",
                                            IntPtr.Zero);

                if (p != IntPtr.Zero)
                {
                    workerw = W32.FindWindowEx(IntPtr.Zero,
                                               tophandle,
                                               "WorkerW",
                                               IntPtr.Zero);

                }


                return true;
            }), IntPtr.Zero);
        }
    }
}