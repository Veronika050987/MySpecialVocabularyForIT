using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySpecialVocabularyForIT.Components.Models;

namespace MySpecialVocabularyForIT.Data
{
    public class MySpecialVocabularyForITContext : DbContext
    {
        public MySpecialVocabularyForITContext (DbContextOptions<MySpecialVocabularyForITContext> options)
            : base(options)
        {
        }

        public DbSet<MySpecialVocabularyForIT.Components.Models.Word> Words { get; set; } = default!;
    }
}
