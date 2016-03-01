using ShopAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAssistant.Repository
{
    public interface INapiAdatokRepository
    {
        Task<List<AruKeszlet>> GetAruKeszlet();
        Task<List<AruKategoria>> GetAruKategoriak();
        Task<List<ErtekesitesReszlet>> GetAruErtekesitesekReszlet(int aruID);
        Task<Ertekesites> GetErtekesitesDatum(int ertekesitesID);
        Task<AruKategoria> GetKategoria(int arukategoriaID);
    }
}
