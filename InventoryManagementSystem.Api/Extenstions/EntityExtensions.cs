using System;
using InventoryManagementSystem.Api.Models;

namespace InventoryManagementSystem.Api.Extensions
{
    public static class EntityExtensions
    {
        public static T Set<T>(this T entity, Action<T> action) where T : IEntity{
            action(entity);
            return entity;
        }
    }
}