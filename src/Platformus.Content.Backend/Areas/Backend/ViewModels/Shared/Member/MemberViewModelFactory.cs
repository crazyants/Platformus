﻿// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Shared
{
  public class MemberViewModelFactory : ViewModelFactoryBase
  {
    public MemberViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public MemberViewModel Create(Member member, Object @object, int? objectId = null)
    {
      IEnumerable<RelationViewModel> relations = null;
      PropertyViewModel property = null;

      if (member.PropertyDataTypeId != null)
      {
        if (@object == null)
          property = new PropertyViewModelFactory(this.handler).Create(
            new Property() { ObjectId = @object == null ? (int?)null : @object.Id  }
          );

        else
        {
          Property p = this.handler.Storage.GetRepository<IPropertyRepository>().WithObjectIdAndMemberId(@object.Id, member.Id);

          property = new PropertyViewModelFactory(this.handler).Create(
            p == null ? new Property() { ObjectId = @object == null ? (int?)null : @object.Id } : p
          );
        }
      }

      else if (member.RelationClassId != null)
      {
        if (@object == null && objectId == null)
          relations = new RelationViewModel[] { };

        else if (@object == null && objectId != null)
          relations = new RelationViewModel[] { new RelationViewModel() { PrimaryId = (int)objectId } };

        else relations = this.handler.Storage.GetRepository<IRelationRepository>().FilteredByMemberIdAndForeignId(member.Id, @object.Id).Select(
          r => new RelationViewModelFactory(this.handler).Create(r)
        );
      }

      return new MemberViewModel()
      {
        Id = member.Id,
        Name = member.Name,
        Position = member.Position,
        PropertyDataType = member.PropertyDataTypeId == null ? null : new DataTypeViewModelFactory(this.handler).Create(
          this.handler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId)
        ),
        IsPropertyLocalizable = member.IsPropertyLocalizable == true,
        Property = property,
        RelationClass = member.RelationClassId == null ? null : new ClassViewModelFactory(this.handler).Create(
          this.handler.Storage.GetRepository<IClassRepository>().WithKey((int)member.RelationClassId)
        ),
        IsRelationSingleParent = member.IsRelationSingleParent == true,
        Relations = relations
      };
    }
  }
}