namespace IceCream.Sales.UnitTests.Domain;

public class EntityTests
{
    [Fact]
    public void Equals_EqualEntities_ReturnsTrue()
    {
        var entity1 = new EntityA(10);
        var entity2 = new EntityA(10);
        var entity2Object = (object)entity2;

        Assert.True(entity1.Equals(entity2));

        Assert.True(Equals(entity1, entity2));

        Assert.True(entity1.Equals(entity2Object));

        Assert.True(entity1 == entity2);

        Assert.False(entity1 != entity2);
    }

    [Fact]
    public void Equals_NonEqualEntities_ReturnsFalse()
    {
        var entity1 = new EntityA(10);
        var entity2 = new EntityA(20);
        var entity3 = new EntityB(20);
        var entity2Object = (object)entity2;

        Assert.False(entity1.Equals(entity2));

        Assert.False(Equals(entity1, entity2));

        Assert.False(entity1.Equals(entity2Object));

        Assert.False(entity1 == entity2);

        Assert.True(entity1 != entity2);

        Assert.False(entity2.Equals(entity3));
    }

    private class EntityA : Entity<int> 
    {
        public EntityA(int id) : base(id)
        {
        }
    }

    private class EntityB : Entity<int> 
    {
        public EntityB(int id) : base(id)
        {
        }
    }
}