namespace IceCream.Sales.Infrastructure.DbContexts.EntityTypeConfigurations;

internal class BuyerEntityTypeConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.ToTable("buyers");

        builder.HasKey(x => x.Id);

        builder.Ignore(b => b.DomainEvents);
    }
}
