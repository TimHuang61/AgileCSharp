using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LSP
{
    class Program
    {
        static void Main(string[] args)
        {
            // 遵守LSP
            // 1. 子類的方法參數可以逆變
            // 2. 子類的返回的類型可以協變
            // 3. 不允許有新的expection
            IEqualityComparer<User> equalityComparer = new EqualityComparer();
            var user1 = new User();
            var user2 = new User();
            equalityComparer.Equal(user1, user2);

            Console.Read();
        }
    }

    public class Entity
    {
        public Guid ID { get; set; }

        public string Name { get; set; }
    }

    public class User : Entity
    {
        public string EmailAddress { get; set; }

        public DateTime DateOfBirth { get; set; }
    }

    public class EntityRepository
    {
        public virtual Entity GetByID(Guid id)
        {
            return new Entity();
        }
    }

    public interface IEntityRepository<out TEntity> where TEntity : Entity
    {
        TEntity GetByID(Guid id);
    }

    // 透過泛型的協變，讓子類回傳自己的型別
    public class UserRepository : IEntityRepository<User>
    {
        public User GetByID(Guid id)
        {
            return new User();
        }
    }

    public interface IEqualityComparer<in TEntity> where TEntity : Entity
    {
        bool Equal(TEntity left, TEntity right);
    }

    public class EqualityComparer : IEqualityComparer<Entity>
    {
        public bool Equal(Entity left, Entity right)
        {
            return left.ID == right.ID;
        }
    }
}
