using ShopAssistant.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Threading.Tasks;

namespace ShopAssistant.Repository
{
    public class NapiAdatokRepository : INapiAdatokRepository
    {
        KiskereskedoEntities context = new KiskereskedoEntities();

        public Task<List<AruKeszlet>> GetAruKeszlet()
        {
            return context.AruKeszletek.ToListAsync();
        }
        public Task<List<AruKategoria>> GetAruKategoriak()
        {
            return context.AruKategoriak.ToListAsync();
        }
        public Task<List<ErtekesitesReszlet>> GetAruErtekesitesekReszlet(int aruID)
        {
            return context.ErtekesitesReszletek.Where(c => c.AruID == aruID).ToListAsync();
        }
        public Task<Ertekesites> GetErtekesitesDatum (int ertekesitesID)
        {
            return context.Ertekesitesek.FirstOrDefaultAsync(c => c.ErtekesitesID == ertekesitesID);
        }
        public Task<AruKategoria> GetKategoria(int arukategoriaID)
        {
            return context.AruKategoriak.FirstOrDefaultAsync(c => c.AruKategoriaID == arukategoriaID);
        }

    }
}
