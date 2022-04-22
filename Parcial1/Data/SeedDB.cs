using Parcial1.Data.Entities;

namespace Parcial1.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTicketsAsync();
            await CheckEntrancesAsync();
        }

        private async Task CheckTicketsAsync()
        {
            if (!_context.Tickets.Any())
            {
                for (int i = 1; i <= 5000; i++)
                {
                    _context.Tickets.Add(new Ticket 
                    {
 
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckEntrancesAsync()
        {
            if (!_context.Entrances.Any())
            {
                _context.Entrances.Add(new Entrance { Description = "Norte" });
                _context.Entrances.Add(new Entrance { Description = "Sur" });
                _context.Entrances.Add(new Entrance { Description = "Oriental" });
                _context.Entrances.Add(new Entrance { Description = "Occidental" });
            }

            await _context.SaveChangesAsync();
        }
    }
}

