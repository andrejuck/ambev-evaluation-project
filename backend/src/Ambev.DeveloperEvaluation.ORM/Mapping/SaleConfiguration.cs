using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(s => s.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.HasIndex(s => s.SaleNumber).IsUnique();

            builder.Property(s => s.SaleNumber)
                .IsRequired();

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(s => s.SaleStatus)
                .IsRequired();

            builder.HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey("CustomerId")
                .IsRequired();

            builder.HasOne(s => s.Branch)
                .WithMany()
                .HasForeignKey("BranchId")
                .IsRequired();

            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey("SaleId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.UpdatedAt)
                .IsRequired(false);

            builder.Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
