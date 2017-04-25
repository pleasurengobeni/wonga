using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refactor.Entities;

namespace Refactor
{
    public class Database
    {
        private static readonly object _locker = new object();
        private static Database _instance; 
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new Database();
                            _instance.Seed();
                        }
                    }
                }

                return _instance;
            }
        }

        private IList<object> _objects = new List<object>();

        public IQueryable<TEntity> Items<TEntity>()
        {
            return _objects.OfType<TEntity>().AsQueryable();
        }

        public IList<TEntity> Query<TEntity>(Func<TEntity, bool> query)
        {
            return _objects.OfType<TEntity>().Where(query).ToList();
        }

        public void Add<TEntity>(TEntity entity)
        {
            _objects.Add(entity);
        }

        public void Seed()
        {
            Add(new StockItem()
            {
                StockCode = "DRML-3000-BR"
            });

            Add(new StockItem()
            {
                StockCode = "SS-SSD-850-250"
            });


        }
    }
}
