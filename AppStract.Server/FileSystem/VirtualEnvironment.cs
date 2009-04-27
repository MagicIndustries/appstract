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

using System;
using System.IO;
using AppStract.Core.Logging;

namespace AppStract.Server.FileSystem
{
  /// <summary>
  /// Provides information about, and means to manipulate,
  /// the current virtual environment and platform.
  /// </summary>
  public static class VirtualEnvironment
  {

    #region Variables

    /// <summary>
    /// The number generator to use for pseudo-random numbers.
    /// </summary>
    private static readonly Random _randomGenerator;

    #endregion

    #region Constructors

    static VirtualEnvironment()
    {
      _randomGenerator = new Random(DateTime.Now.Millisecond);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets the path to the system virtual folder identified by the <see cref="VirtualFolder"/> specified.
    /// The returned path is relative to the file system's root directory.
    /// </summary>
    /// <remarks> The returned path is not guaranteed to exist.</remarks>
    /// <param name="virtualFolder">An enumerated constant that identifies a system virtual folder.</param>
    /// <returns>The path to the specified system virtual folder.</returns>
    public static string GetFolderPath(VirtualFolder virtualFolder)
    {
      switch (virtualFolder)
      {
        case VirtualFolder.ProgramFiles:
          return @"ProgramFiles\";
        case VirtualFolder.UserData:
          return @"UserData\";
        case VirtualFolder.UserDocuments:
          return @"UserData\Documents\";
        case VirtualFolder.UserMusic:
          return @"UserData\Music\";
        case VirtualFolder.UserPictures:
          return @"UserData\Pictures\";
        case VirtualFolder.Temporary:
          return @"Temporary\";
        case VirtualFolder.Other:
          return @"Other\";
        case VirtualFolder.System:
          return @"System\";
        case VirtualFolder.System32:
          return @"System\System32\";
        case VirtualFolder.StartMenu:
          return @"StartMenu\";
        case VirtualFolder.ApplicationData:
          return @"ApplicationData\";
      }
      return null;
    }

    /// <summary>
    /// Returns the full path of a uniquely named file, in the specified directory.
    /// The file is created if requested.
    /// </summary>
    /// <remarks>The file is not guaranteed to be unique if the file isn't created by this method.</remarks>
    /// <exception cref="IOException">
    /// This method was unable to create a file.
    /// </exception>
    /// <param name="directory">The directory in which the filename must be unique.</param>
    /// <param name="createFile">Indicates whether the file must be created.</param>
    /// <returns>The full path of the file.</returns>
    public static string GetUniqueFile(string directory, bool createFile)
    {
      if (!directory.EndsWith(@"\"))
        directory = directory + @"\";
      string filename = directory + _randomGenerator.Next(10000, 999999) + "_RND.dat";
      while (File.Exists(filename))
        filename = directory + _randomGenerator.Next(10000, 999999) + "_RND.dat";
      if (createFile)
        File.Create(filename).Close();
      return filename;
    }

    /// <summary>
    /// Tries to create all system-folders, as defined in <see cref="VirtualFolder"/>.
    /// </summary>
    /// <param name="rootFolder">Rootfolder for the virtual folders.</param>
    /// <returns>True if all folders are created; False if the creation of one or more folders failed.</returns>
    public static bool CreateVirtualFolders(string rootFolder)
    {
      GuestCore.Log(new LogMessage(LogLevel.Information,
                                   "Creating system folders for a virtual environment with root \"{0}\"", rootFolder));
      bool failed = false;
      foreach (VirtualFolder virtualFolder in Enum.GetValues(typeof (VirtualFolder)))
        failed = TryCreateDirectory(Path.Combine(rootFolder, GetFolderPath(virtualFolder)))
                   ? failed
                   : true;
      return failed;
    }

    /// <summary>
    /// Tries to create the directory, specified by <paramref name="path"/>.
    /// </summary>
    /// <param name="path">Directory to create.</param>
    /// <returns>True if the directory is created; False, otherwise.</returns>
    public static bool TryCreateDirectory(string path)
    {
      try
      {
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
      }
      catch (IOException)
      {
        GuestCore.Log(new LogMessage(LogLevel.Warning, "Failed to create directory: " + path));
        return false;
      }
      return true;
    }

    #endregion

  }
}