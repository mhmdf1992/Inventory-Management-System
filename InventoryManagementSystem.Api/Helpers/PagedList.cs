using System;
using System.Collections.Generic;
using InventoryManagementSystem.Api.Models;

namespace InventoryManagementSystem.Api.Helpers
{
    public class PagedList<T> : List<T> where T : IEntity
    {
        public PagedList(IEnumerable<T> list): base(list){}
        public int Total {get; set;}
        public PagedList<T> Set(Action<PagedList<T>> action){
            action(this);
            return this;
        }
    }
}