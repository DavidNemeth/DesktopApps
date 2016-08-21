﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernChattingClient.Pages.Chat
{
    /// <summary>
    /// Interaction logic for MainRoom.xaml
    /// </summary>
    public partial class MainRoom : UserControl
    {
        private static MainRoom _this;

        public MainRoom()
        {
            InitializeComponent();
            _this = this;
        }

        public static MainRoom GetInstance()
        {
            return _this;
        }
    }
}
