using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => !p.Is�R��);
        }

        public IQueryable<Product> All(bool showAll)
         {
             if (showAll)
             {
                 return base.All();
             }
             else
             {
                 return this.All();
             }
         }

        public Product Get�浧���ByProductID(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> GetProduct�C���Ҧ����(bool Active, bool showAll=false)
        {
            IQueryable<Product> all = this.All();
            if (showAll)
                all = base.All();

            return all
                .Where(p => p.Active.HasValue && p.Active.Value == Active && p.Is�R�� == false)
                .OrderByDescending(p => p.ProductId).Take(10);
        }

        public void Update(Product product)
        {
            //db.Entry(product).State = EntityState.Modified;
            //db.SaveChanges();

            this.UnitOfWork.Context.Entry(product).State = EntityState.Modified;
        }

        public override void Delete(Product entity)
        {
            //base.Delete(entity);
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;    //�j���������� ==> �̦n�g�b controller
            entity.Is�R�� = true;
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}