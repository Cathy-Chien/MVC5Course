using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{
    public class ClientRepository : EFRepository<Client>, IClientRepository
    {
        public Client Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ClientId == id);
        }
        public IQueryable<Client> All(bool isAll = false)
        {
            if (isAll)
            {
                return base.All();
            }
            return base.All().Where(p => p.IsDelete !=true);
        }
        public override void Delete(Client entity)
        {
            entity.IsDelete = true;
        }
    }

    public interface IClientRepository : IRepository<Client>
    {

    }
}