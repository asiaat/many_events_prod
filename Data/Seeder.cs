using System;
using ManyEvents.Models;
using Serilog;

namespace ManyEvents.Data
{
	public class Seeder
	{
        private readonly DBContext _context;

        public Seeder(DBContext context)
		{
            _context = context;
        }

        public void SeedFeeTypes()
        {
            Log.Information("Seeder::SeedFeeTypes starts ..");
            
            var feedCash = new MFeeType();
            feedCash.SetName("sularaha");
            feedCash.SetRemarks("EUR");
            _context.Add(feedCash);

            var feeCards = new MFeeType();
            feeCards.SetName("pangaülekanne");
            feeCards.SetRemarks("EUR");
            _context.Add(feeCards);


            _context.SaveChanges();

            Log.Information("Seeder::SeedFeeTypes ends ..");
        }

        public void SeedPersons()
        {
            Log.Information("Seeder::SeedPersons starts ..");

            var p1 = new MPerson();
            p1.SetFirstName("Heino");
            p1.SetLastName("Muidugi");
            p1.SetPersonalCode("38512217079");
            _context.Add(p1);

            var p2 = new MPerson();
            p2.SetFirstName("Aita-Kivi");
            p2.SetLastName("Weerema");
            p2.SetPersonalCode("48008203755");
            //_context.Add(p2);

            _context.SaveChanges();

            Log.Information("Seeder::SeedPersons ends ..");
        }
	}
}

