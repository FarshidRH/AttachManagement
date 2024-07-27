using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttachManagement.Infrastructure.Data.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category");

        builder.Property<int>("id");

        builder.Property<int?>("parent_id");
        builder.HasMany<Category>("_subCategories").WithOne().HasForeignKey("parent_id");

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(128);

        builder.Ignore(x => x.SubCategories);
        builder.Ignore(x => x.Attachments);

        builder.HasKey("id");
    }
}
