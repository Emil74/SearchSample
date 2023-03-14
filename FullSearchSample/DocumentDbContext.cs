using FullSearchSample.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearchSample
{
    public class DocumentDbContext : DbContext
    {
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<WordDocument> WordDocuments { get; set; }
        public DocumentDbContext(DbContextOptions options) : base(options) { }

    }
}
