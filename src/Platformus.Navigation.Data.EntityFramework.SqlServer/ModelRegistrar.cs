﻿// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Data.EntityFramework.SqlServer
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CachedMenu>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.MenuId });
          etb.ForSqlServerToTable("CachedMenus");
        }
      );

      modelBuilder.Entity<Menu>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Menus");
        }
      );

      modelBuilder.Entity<MenuItem>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("MenuItems");
        }
      );
    }
  }
}