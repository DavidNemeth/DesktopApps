using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmersiveBackground.ViewModel
{
    public class HomeViewModel
    {
        public RelayCommand Loadfile { get; set; }
        public HomeViewModel()
        {
            CreateCommands();
        }
        private void CreateCommands()
        {
            Loadfile = new RelayCommand(OnLoadfile);
        }

        private void OnLoadfile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All files (*.*)|*.*";

            bool? result = fileDialog.ShowDialog();
            //TODO
        }

        private void SetBackground()
        {

        }
    }
}
