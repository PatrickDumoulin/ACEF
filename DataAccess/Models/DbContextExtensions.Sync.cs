﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public static class DbContextExtensions
    {
        public static List<T> SqlQuery<T>(this DbContext db, string sql, object[] parameters = null) where T : class
        {
            if (parameters is null)
            {
                parameters = new object[] { };
            }

            if (typeof(T).GetProperties().Any())
            {
                return db.Set<T>().FromSqlRaw(sql, parameters).ToList();
            }
            else
            {
                db.Database.ExecuteSqlRaw(sql, parameters);
                return default;
            }
        }
    }

    public class OutputParameter<TValue>
    {
        private bool _valueSet = false;

        public TValue _value;

        public TValue Value
        {
            get
            {
                if (!_valueSet)
                    throw new InvalidOperationException("Value not set.");

                return _value;
            }
        }

        internal void SetValue(object value)
        {
            _valueSet = true;

            _value = null == value || Convert.IsDBNull(value) ? default(TValue) : (TValue)value;
        }
    }
}
