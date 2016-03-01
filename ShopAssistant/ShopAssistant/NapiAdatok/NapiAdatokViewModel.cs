using ShopAssistant.Base;
using ShopAssistant.Model;
using ShopAssistant.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAssistant.NapiAdatok
{
    class NapiAdatokViewModel : BindableBase
    {
        private INapiAdatokRepository _repo;
        public NapiAdatokViewModel(INapiAdatokRepository repo)
        {
            _repo = repo;
            AruKeszlet = new ObservableCollection<AruKeszlet>(_repo.GetAruKeszlet().Result);
        }
        private ObservableCollection<ErtekesitesReszlet> ertekesitesreszlet;
        public ObservableCollection<ErtekesitesReszlet> ErtekesitesReszletek
        {
            get { return this.ertekesitesreszlet; }
            set { SetProperty(ref ertekesitesreszlet, value); }
        }
        private ObservableCollection<AruKeszlet> arukeszlet;
        public ObservableCollection<AruKeszlet> AruKeszlet
        {
            get { return this.arukeszlet; }
            set { SetProperty(ref arukeszlet, value); }
        }
        private ObservableCollection<AruKategoria> kategoriak;
        public ObservableCollection<AruKategoria> Kategoriak
        {
            get { return this.kategoriak; }
            set { SetProperty(ref kategoriak, value); }
        }

        private List<AruKategoria> _allKategoria;
        public async void LoadKategoriak()
        {
            _allKategoria = await _repo.GetAruKategoriak();
        }
    }
}
