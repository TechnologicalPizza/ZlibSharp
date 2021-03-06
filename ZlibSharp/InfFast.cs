// Generated by Sichem at 2020-09-30 18:30:53

using System;
using System.Runtime.InteropServices;
using static CRuntime;

namespace ZlibSharp
{
	unsafe class InfFast
	{
		public const int CODES = 0;
		public const int LENS = 1;
		public const int DISTS = 2;
		public const int HEAD = 16180;
		public const int FLAGS = 16181;
		public const int TIME = 16182;
		public const int OS = 16183;
		public const int EXLEN = 16184;
		public const int EXTRA = 16185;
		public const int NAME = 16186;
		public const int COMMENT = 16187;
		public const int HCRC = 16188;
		public const int DICTID = 16189;
		public const int DICT = 16190;
		public const int TYPE = 16191;
		public const int TYPEDO = 16192;
		public const int STORED = 16193;
		public const int COPY_ = 16194;
		public const int COPY = 16195;
		public const int TABLE = 16196;
		public const int LENLENS = 16197;
		public const int CODELENS = 16198;
		public const int LEN_ = 16199;
		public const int LEN = 16200;
		public const int LENEXT = 16201;
		public const int DIST = 16202;
		public const int DISTEXT = 16203;
		public const int MATCH = 16204;
		public const int LIT = 16205;
		public const int CHECK = 16206;
		public const int LENGTH = 16207;
		public const int DONE = 16208;
		public const int BAD = 16209;
		public const int MEM = 16210;
		public const int SYNC = 16211;
		public static sbyte*[] z_errmsg = new sbyte[10];
		[StructLayout(LayoutKind.Sequential)]
		public class z_stream_s
	{
		public byte* next_in;
		public uint avail_in;
		public int total_in;
		public byte* next_out;
		public uint avail_out;
		public int total_out;
		public string msg;
		public internal_state state;
		public zalloc_delegate zalloc;
		public zfree_delegate zfree;
		public void * opaque;
		public int data_type;
		public int adler;
		public int reserved;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct gz_header_s
	{
		public int text;
		public int time;
		public int xflags;
		public int os;
		public byte* extra;
		public uint extra_len;
		public uint extra_max;
		public byte* name;
		public uint name_max;
		public byte* comment;
		public uint comm_max;
		public int hcrc;
		public int done;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct gzFile_s
	{
		public uint have;
		public byte* next;
		public long pos;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct code
	{
		public byte op;
		public byte bits;
		public ushort val;
		}
		[StructLayout(LayoutKind.Sequential)]
		public class inflate_state
	{
		public z_stream_s strm;
		public inflate_mode mode;
		public int last;
		public int wrap;
		public int havedict;
		public int flags;
		public uint dmax;
		public int check;
		public int total;
		public gz_header_s* head;
		public uint wbits;
		public uint wsize;
		public uint whave;
		public uint wnext;
		public byte* window;
		public int hold;
		public uint bits;
		public uint length;
		public uint offset;
		public uint extra;
		public code* lencode;
		public code* distcode;
		public uint lenbits;
		public uint distbits;
		public uint ncode;
		public uint nlen;
		public uint ndist;
		public uint have;
		public code* next;
		public fixed ushort lens[320];
		public fixed ushort work[288];
		public code[] codes;
		public int sane;
		public int back;
		public uint was;
		}
		public static void inflate_fast(z_stream_s strm, uint start)
		{
			inflate_state state;
			byte* _in_;
			byte* last;
			byte* _out_;
			byte* beg;
			byte* end;
			uint wsize = 0;
			uint whave = 0;
			uint wnext = 0;
			byte* window;
			int hold = 0;
			uint bits = 0;
			code* lcode;
			code* dcode;
			uint lmask = 0;
			uint dmask = 0;
			code here = new code();
			uint op = 0;
			uint len = 0;
			uint dist = 0;
			byte* from;
			state = (inflate_state)strm.state;
			_in_ = strm.next_in;
			last = _in_ + (strm.avail_in - 5);
			_out_ = strm.next_out;
			beg = _out_ - (start - strm.avail_out);
			end = _out_ + (strm.avail_out - 257);
			wsize = state->wsize;
			whave = state->whave;
			wnext = state->wnext;
			window = state->window;
			hold = state->hold;
			bits = state->bits;
			lcode = state->lencode;
			dcode = state->distcode;
			lmask = (uint)((1U << state->lenbits) - 1);
			dmask = (uint)((1U << state->distbits) - 1);
			do {
if (bits < 15) {
hold += (int)(*_in_++ << bits);bits += 8;
                    hold += (int)(*_in_++ << bits);bits += 8;
                }
here = lcode[hold & lmask];
                dolen:;
op = here.bits;
                hold >>= op;bits -= op;
                op = here.op;
                if (op == 0) {
*_out_++ = (byte)here.val;}
 else if ((op & 16) != 0) {
len = here.val;
                    op &= 15;
                    if (op != 0) {
if (bits < op) {
hold += (int)(*_in_++ << bits);bits += 8;
                        }
len += (uint)((uint)hold & ((1U << op) - 1));hold >>= op;bits -= op;
                    }
if (bits < 15) {
hold += (int)(*_in_++ << bits);bits += 8;
                        hold += (int)(*_in_++ << bits);bits += 8;
                    }
here = dcode[hold & dmask];
                    dodist:;
op = here.bits;
                    hold >>= op;bits -= op;
                    op = here.op;
                    if ((op & 16) != 0) {
dist = here.val;
                        op &= 15;
                        if (bits < op) {
hold += (int)(*_in_++ << bits);bits += 8;
                            if (bits < op) {
hold += (int)(*_in_++ << bits);bits += 8;
                            }
}
dist += (uint)((uint)hold & ((1U << op) - 1));hold >>= op;bits -= op;
                        op = (uint)(_out_ - beg);if (dist > op) {
op = dist - op;
                            if (op > whave) {
if (state->sane != 0) {
strm.msg = "invalid distance too far back";state->mode = (inflate_mode)BAD;break;}
}
from = window;if (wnext == 0) {
from += wsize - op;if (op < len) {
len -= op;
                                    do {
*_out_++ = *from++;
                                    }
 while ((--op) != 0);from = _out_ - dist;}
}
 else if (wnext < op) {
from += wsize + wnext - op;op -= wnext;
                                if (op < len) {
len -= op;
                                    do {
*_out_++ = *from++;
                                    }
 while ((--op) != 0);from = window;if (wnext < len) {
op = wnext;
                                        len -= op;
                                        do {
*_out_++ = *from++;
                                        }
 while ((--op) != 0);from = _out_ - dist;}
}
}
 else {
from += wnext - op;if (op < len) {
len -= op;
                                    do {
*_out_++ = *from++;
                                    }
 while ((--op) != 0);from = _out_ - dist;}
}
while (len > 2) {
*_out_++ = *from++;
                                *_out_++ = *from++;
                                *_out_++ = *from++;
                                len -= 3;
                            }if (len != 0) {
*_out_++ = *from++;
                                if (len > 1) *_out_++ = *from++;
                            }
}
 else {
from = _out_ - dist;do {
*_out_++ = *from++;
                                *_out_++ = *from++;
                                *_out_++ = *from++;
                                len -= 3;
                            }
 while (len > 2);if (len != 0) {
*_out_++ = *from++;
                                if (len > 1) *_out_++ = *from++;
                            }
}
}
 else if ((op & 64) == 0) {
here = dcode[here.val + (hold & ((1U << op) - 1))];
                        goto dodist;}
 else {
strm.msg = "invalid distance code";state->mode = (inflate_mode)BAD;break;}
}
 else if ((op & 64) == 0) {
here = lcode[here.val + (hold & ((1U << op) - 1))];
                    goto dolen;}
 else if ((op & 32) != 0) {
state->mode = (inflate_mode)TYPE;break;}
 else {
strm.msg = "invalid literal/length code";state->mode = (inflate_mode)BAD;break;}
}
 while ((_in_ < last) && (_out_ < end));
			len = bits >> 3;
			_in_ -= len;
			bits -= len << 3;
			hold &= (int)((1U << bits) - 1);
			strm.next_in = _in_;
			strm.next_out = _out_;
			strm.avail_in = (uint)(_in_ < last?5 + (last - _in_):5 - (_in_ - last));
			strm.avail_out = (uint)(_out_ < end?257 + (end - _out_):257 - (_out_ - end));
			state->hold = hold;
			state->bits = bits;
			return;
		}

}
}