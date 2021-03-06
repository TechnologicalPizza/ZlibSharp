// Generated by Sichem at 2020-09-30 18:30:50

using System;
using System.Runtime.InteropServices;
using static CRuntime;
using static ZlibSharp.ZUtil;

namespace ZlibSharp
{
    unsafe class GZClose
    {
        public static int gzclose(gzFile_s* file)
        {
            gz_state* state;
            if (file == ((void*)0))
                return -2;
            state = (gz_state*)file;
            return (int)(state->mode == 7247 ? gzclose_r(file) : gzclose_w(file));
        }
    }
}