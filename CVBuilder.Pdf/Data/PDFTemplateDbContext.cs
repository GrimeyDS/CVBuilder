using CVBuilder.Pdf.Data.Entities;
using CVBuilder.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Pdf.Data;

internal sealed class PDFTemplateDbContext : DbContextBase<PDFTemplateDbContext>
{
    public DbSet<PDFTemplateEntity> PDFTemplates { get; set; }

    public PDFTemplateDbContext()
    {
    }

    public PDFTemplateDbContext(DbContextOptions<PDFTemplateDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}