﻿// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Shared
{
  public class DataTypeViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string JavaScriptEditorClassName { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
  }
}