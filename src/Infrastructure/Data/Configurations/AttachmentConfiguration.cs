using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttachManagement.Infrastructure.Data.Configurations;

internal class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("attachment");

        builder.Property<Guid>("_id").HasColumnName("id");

        builder.Property<int>("category_id");
        builder.HasOne<Category>().WithMany("_attachments").HasForeignKey("category_id");

        builder.Property(x => x.FileName)
            .HasColumnName("file_name")
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.FileType)
            .HasColumnName("file_type")
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(8)
            .IsUnicode(false);

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(-1);

        builder.Property(x => x.CreatedOnUtc).HasColumnName("created_date_utc");
        builder.Property(x => x.LastModifiedOnUtc).HasColumnName("modified_date_utc");

        builder.HasKey("_id");
    }
}
