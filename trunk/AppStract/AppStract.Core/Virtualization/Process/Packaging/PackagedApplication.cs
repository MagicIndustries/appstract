﻿#region Copyright (C) 2008-2009 Simon Allaeys

/*
    Copyright (C) 2008-2009 Simon Allaeys
 
    This file is part of AppStract

    AppStract is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    AppStract is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with AppStract.  If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

using System.Collections.Generic;

namespace AppStract.Core.Virtualization.Process.Packaging
{
  /// <summary>
  /// Represents a packaged application. The data can be used to create an instance of
  /// <see cref="AppStract.Core.Data.Application.ApplicationData"/> with.
  /// </summary>
  public class PackagedApplication
  {

    #region Variables

    private readonly List<string> _executables;
    private readonly string _outputLocation;
    private readonly string _relDbFileSystem;
    private readonly string _relDbRegistry;

    #endregion

    #region Properties

    /// <summary>
    /// Gets all executables that were detected during packaging.
    /// </summary>
    public IList<string> Executables
    {
      get { return _executables; }
    }

    /// <summary>
    /// Gets the location of the packaged application.
    /// </summary>
    public string OutputLocation
    {
      get { return _outputLocation; }
    }

    /// <summary>
    /// Gets the location of the database containing the file table;
    /// Relative to <see cref="OutputLocation"/>.
    /// </summary>
    public string FileSystemDatabase
    {
      get { return _relDbFileSystem; }
    }

    /// <summary>
    /// Gets the location of the database containing the registry keys and values;
    /// Relative to <see cref="OutputLocation"/>.
    /// </summary>
    public string RegistryDatabase
    {
      get { return _relDbRegistry; }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="PackagedApplication"/>.
    /// </summary>
    /// <param name="outputLocation">The location of the packaged application.</param>
    /// <param name="executables">All executables that were detected during packaging.</param>
    /// <param name="dbFileSystem">The location of the database containing the file table.</param>
    /// <param name="dbRegistry">he location of the database containing the registry keys and values.</param>
    public PackagedApplication(string outputLocation, IEnumerable<string> executables, string dbFileSystem, string dbRegistry)
    {
      _outputLocation = outputLocation;
      _executables = new List<string>(executables);
      /// BUG: Verify that these paths are relative to outputLocation!
      _relDbFileSystem = dbFileSystem;
      _relDbRegistry = dbRegistry;
    }

    #endregion

  }
}