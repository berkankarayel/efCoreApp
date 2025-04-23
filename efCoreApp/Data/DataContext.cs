using Microsoft.EntityFrameworkCore;
using System;

namespace efCoreApp.Data
{
	public class DataContext:DbContext
	{

		public DataContext(DbContextOptions<DataContext> options) : base(options) {}


		public DbSet<Kurs> Kurslar { get; set; }
		public DbSet<KursKayit> KursKayitlar { get; set; }
		public DbSet<Ogrenci> Ogrenciler { get; set; }



	}
}
