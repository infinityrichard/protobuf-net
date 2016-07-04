﻿using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Examples
{
    public static class PEVerify
    {
        public static bool AssertValid(string path)
        {
#if COREFX
            return true;
#else
            // note; PEVerify can be found %ProgramFiles(x86)%\Microsoft SDKs\Windows\v6.0A\bin
            const string exePath = @"%ProgramFiles(x86)%\Microsoft SDKs\Windows\v6.0A\bin\PEVerify.exe";
            using (Process proc = Process.Start(exePath, path))
            {
                if (proc.WaitForExit(10000))
                {
                    Assert.AreEqual(0, proc.ExitCode, path);
                    return proc.ExitCode == 0;
                }
                else
                {
                    proc.Kill();
                    throw new TimeoutException();
                }
            }
#endif
        }
    }
}
