using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;
namespace FBD.Models
{

    /// <summary>
    /// There are no comments for FBDEntities in the schema.
    /// </summary>
    public static class DatabaseHelper 
    {
        public static void AttachToOrGet<T>(FBDEntities context, string entitySetName, ref T entity)
                where T : IEntityWithKey
        {
            ObjectStateEntry entry;
            // Track whether we need to perform an attach
            bool attach = false;
            if (
                    context.ObjectStateManager.TryGetObjectStateEntry
                            (
                                    context.CreateEntityKey(entitySetName, entity),
                                    out entry
                            )
                    )
            {
                // Re-attach if necessary
                attach = entry.State == EntityState.Detached;
                // Get the discovered entity to the ref
                entity = (T)entry.Entity;
            }
            else
            {
                // Attach for the first time
                attach = true;
            }
            if (attach)
                context.AttachTo(entitySetName, entity);
        }
    }
}