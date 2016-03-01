using ShopAssistant.Base;
using ShopAssistant.NapiAdatok;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace ShopAssistant
{
    class MainWindowViewModel : BindableBase
    {
        private NapiAdatokViewModel _napiAdatokViewModel;
        private NapiAdatFelvitelViewModel _napiAdatFelvitelViewModel;

        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);

            _napiAdatokViewModel = ContainerHelper.Container.Resolve<NapiAdatokViewModel>();
            _napiAdatFelvitelViewModel = ContainerHelper.Container.Resolve<NapiAdatFelvitelViewModel>();
        }
        private BindableBase currentViewModel;
        public BindableBase CurrentViewModel
        {
            get { return this.currentViewModel; }
            set { SetProperty(ref currentViewModel, value); }
        }
        public RelayCommand<string> NavCommand { get; private set; }
        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "adatfelvitel":
                    CurrentViewModel = _napiAdatFelvitelViewModel;
                    break;
                case "napiadatok":
                default:
                    CurrentViewModel = _napiAdatokViewModel;
                    break;                    
            }
        }
    
    }
}