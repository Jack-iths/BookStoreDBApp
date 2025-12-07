using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreDbFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDbFirst
{
    internal class DbService
    {
        private readonly CompaniesContext context;

        public DbService()
        {
            context = new CompaniesContext();
        }

        public async Task<List<LagerSaldo>> GetInventoryByStoreAsync(int butikId)
        {
            return await context    .LagerSaldos
                .Include(l => l.Bok)
                .Where(l => l.ButikId == butikId)
                .ToListAsync();
        }

        public async Task<List<Butiker>> GetAllStoresAsync()
        {
            return await context.Butikers.ToListAsync();
        }

        public async Task<List<LagerSaldo>> GetInventoryByBookAsync(string isbn13)
        {
            return await context.LagerSaldos
                .Include(l => l.Butik)
                .Where(l => l.Isbn13 == isbn13)
                .ToListAsync();
        }

        public async Task TaBortBok(int butikId, string isbn13)
        {
            var lagerSaldo = await context.LagerSaldos
                .FirstOrDefaultAsync(l => l.ButikId == butikId && l.Isbn13 == isbn13);

            if (lagerSaldo != null)
            {
                context.LagerSaldos.Remove(lagerSaldo);
                await context.SaveChangesAsync();
            }
        }

        public async Task LäggatillBok(int butikId, string isbn13, int antal)
        {
            var lagerSaldo = new LagerSaldo
            {
                ButikId = butikId,
                Isbn13 = isbn13,
                Antal = antal
            };
            context.LagerSaldos.Add(lagerSaldo);
            await context.SaveChangesAsync();
        }

        public async Task UppdateraBokantal (int butikId, string isbn13, int nyttAntal)
        {
            var lagerSaldo = await context.LagerSaldos
                .FirstOrDefaultAsync(l => l.ButikId == butikId && l.Isbn13 == isbn13);
            if (lagerSaldo != null)
            {
                lagerSaldo.Antal = nyttAntal;
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Böcker>> GetAllBooksAsync()
        {
            return await context.Böckers
                .Include(b => b.Författar)
                .OrderBy(b => b.Titel)
                .ToListAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
