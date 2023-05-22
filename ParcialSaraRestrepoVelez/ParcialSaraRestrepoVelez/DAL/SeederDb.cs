﻿using ParcialSaraRestrepoVelez.DAL.Entities;

namespace ParcialSaraRestrepoVelez.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        public SeederDb(DataBaseContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //Esta línea me ayuda a crear mi BD de forma automática
            await PopulateTicketsAsync();
        }

        private async Task PopulateTicketsAsync()
        {
           if(!_context.Tickets.Any())
            {
                for(int i = 0; i < 50000; i++)
                {
                    _context.Tickets.Add(new Ticket {IsUsed = false, CreatedDate = DateTime.Now });
                }
                              
            }

            await _context.SaveChangesAsync();
        }
    }
}
