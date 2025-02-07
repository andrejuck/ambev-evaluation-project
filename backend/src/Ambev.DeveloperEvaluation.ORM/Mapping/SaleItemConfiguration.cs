using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(si => si.Quantity)
                .IsRequired();

            builder.Property(si => si.Cancelled)
                .IsRequired();

            builder.HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey("ProductId")
                .IsRequired();

            builder.Ignore(si => si.TotalAmount);
            builder.Ignore(si => si.Discount);
        }
    }
}
