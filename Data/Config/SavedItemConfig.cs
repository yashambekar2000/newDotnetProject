using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewDotnetProject.Data;

namespace NewDotnetProject.Data.Config
{
    public class SavedItemConfig : IEntityTypeConfiguration<SavedItem>
    {
        public void Configure(EntityTypeBuilder<SavedItem> builder)
        {
            builder.ToTable("SavedItems");
           builder.HasKey(si => new { si.UserId, si.ItemId });

        builder.HasOne(si => si.User)
            .WithMany(u => u.SavedItems)
            .HasForeignKey(si => si.UserId);

        builder.HasOne(si => si.Item)
            .WithMany(i => i.SavedItems)
            .HasForeignKey(si => si.ItemId);
        }
    }
}