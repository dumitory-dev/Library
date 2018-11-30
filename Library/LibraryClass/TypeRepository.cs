﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass
{
    internal class TypeRepository : IRepository<Type>
    {
        private static TypeRepository _instance;
        private readonly DataBaseContext _baseContext = DataBaseContext.GetInstance();
        private TypeRepository() { }
        public static TypeRepository GetInstance()
        {
            return _instance ?? (_instance = new TypeRepository());
        }
        public void Add(Type type)
        {
            _baseContext.Types.Add(type);
            _baseContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _baseContext.Types.Remove(Get(id));
            _baseContext.SaveChanges();
        }

        public Type Get(int id)
        {
            return _baseContext.Types.FirstOrDefault(type => type.Id == id);
        }

        public IEnumerable<Type> GetAll()
        {
            return _baseContext.Types.ToList();
        }

        public void Update(Type newType)
        {
            var changeable = Get(newType.Id);
            if (changeable == null) return;
            changeable.Name = newType.Name;

            _baseContext.Entry(changeable).State = System.Data.Entity.EntityState.Modified;
            _baseContext.SaveChanges();
        }
    }
}