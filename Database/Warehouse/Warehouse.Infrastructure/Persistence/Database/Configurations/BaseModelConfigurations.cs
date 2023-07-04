using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Core.Models;

namespace Warehouse.Infrastructure.Persistence.Database.Configurations;

public class BaseModelConfigurations:IEntityTypeConfiguration<BaseModel>
{
    public void Configure(EntityTypeBuilder<BaseModel> builder)
    {
        builder.HasIndex(s => s.Id).IsUnique();
        builder.Property(s => s.Id).ValueGeneratedOnAdd();
    }
}