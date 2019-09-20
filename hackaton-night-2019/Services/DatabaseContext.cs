using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace hackaton_night_2019.Services
{
    public interface IDatabaseContext
    {
        LiteCollection<T> Get<T>(string tablename);
        void Set<T>(T entry,string tablename);
    }

    [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
    public class DatabaseContext : IDisposable, IDatabaseContext
    {
        private LiteDatabase _db;

        public DatabaseContext()
        {
            _db = new LiteDatabase("MyData.db");
        }


        public LiteCollection<T> Get<T>(string tablename)
        {
            return _db.GetCollection<T>(tablename);
        }

        public void Set<T>(T entry,string tablename)
        {
            this.Get<T>(tablename).Insert(entry);
        }

        #region IDisposable

        public void Dispose()
        {
            _db?.Dispose();
        }

        #endregion

    }
}
