﻿/* Copyright 2015 Realm Inc - All Rights Reserved
 * Proprietary and Confidential
 */
 
using System;
using System.Diagnostics;

namespace Realms
{
	
    /// <summary>
    /// Per-platform utility functions. A copy of this file exists in each platform project such as Realm.Win32.
    /// </summary>
    public static class InteropConfig
    {
        public static bool Is64Bit
        {
#if REALM_32       
            get {
                Debug.Assert(IntPtr.Size == 4);
                return false;
            }
#elif REALM_64
            get {
                Debug.Assert(IntPtr.Size == 8);
                return true;
            }
#else
            //if this is evaluated every time, a faster way could be implemented. Size is cost when we are running though so perhaps it gets inlined by the JITter
            get { return (IntPtr.Size == 8); }
#endif
        }


#if (DEBUG)
        private const string BuildName = "Debug";
#else
        private const string BuildName = "Release";
#endif

#if REALM_32
        public const string DLL_NAME = "wrappersx86-" + BuildName;
#elif REALM_64
        public const string DLL_NAME = "wrappersx64-" + BuildName;
#else
        public const string DLL_NAME = "** error see InteropConfig.cs DLL_NAME";
#endif

        /// <summary>
        /// Get the location to be used for realms if a full path is not specified
        /// </summary>
        /// <returns>A path to the standard Documents directory for the platform</returns>
        public static string GetDefaultDatabaseLocation()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A full filename path</returns>
        public static string GetDefaultDatabasePath()
        {
            return System.IO.Path.Combine(GetDefaultDatabaseLocation(), Realm.DefaultDatabaseName());
        }
    }
}