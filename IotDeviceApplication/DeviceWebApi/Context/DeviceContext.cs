using DeviceWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceWebApi.Context
{
    public class DeviceContext : DbContext
    {
        public DeviceContext(DbContextOptions<DeviceContext> options) : base(options)
        {

        }

        public DbSet<DeviceMaster> DeviceMaster { get; set; }

        public DbSet<HumidityReading> HumidityReading { get; set; }

        public DbSet<TemperatureReading> TemperatureReading { get; set; }
    }
}
