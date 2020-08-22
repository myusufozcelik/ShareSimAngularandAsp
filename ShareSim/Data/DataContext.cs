using Microsoft.EntityFrameworkCore;
using ShareSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options):base(options)
        {


        }

        public DbSet<Value> Values
        {
            get; set;
        }
        public DbSet<Kategori> Kategoriler
        {
            get; set;
        }
        public DbSet<Kullanici> Kullanicilar
        {
            get; set;
        }
        public DbSet<Resim> Resimler
        {
            get; set;
        }
    }
}
