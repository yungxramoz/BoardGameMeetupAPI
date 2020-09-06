using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardgameMeetup.Data.Access.DAL
{
    class MainDbContext: DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
        :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mappings = MappingsHelper.GetMainMappings();

            foreach (var mapping in mappings)
            {
                mapping.Visit(modelBuilder);
            }
        }
    }
}
