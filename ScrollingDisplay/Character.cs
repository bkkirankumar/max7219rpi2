﻿/*
Author   : KIRAN KUMAR BOLLUKONDA
Email    : BK.KIRAN.KUMAR@GMAIL.COM
           BK.KIRAN.KUMAR@HOTMAIL.COM
Created  : 21-JUNE-2015

Comments : Please note that this code is written only for beginners 
           and I tried to keep as simple as possible. This code allows 
           the beginner to understand the MAX7219 via Raspberry Pi 2 
           SPI with Windows 10.
*/

    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrollingDisplay
{
    public static class Character
    {
        public static byte[] Get(char ch)
        {
            switch (ch)
            {
                case ' ': return new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                case '!': return new byte[] { 0x00, 0x06, 0x5F, 0x5F, 0x06, 0x00, 0x00, 0x00 };
                case '"': return new byte[] { 0x00, 0x07, 0x07, 0x00, 0x07, 0x07, 0x00, 0x00 };
                case '#': return new byte[] { 0x14, 0x7F, 0x7F, 0x14, 0x7F, 0x7F, 0x14, 0x00 };
                case '$': return new byte[] { 0x24, 0x2E, 0x6B, 0x6B, 0x3A, 0x12, 0x00, 0x00 };
                case '%': return new byte[] { 0x46, 0x66, 0x30, 0x18, 0x0C, 0x66, 0x62, 0x00 };
                case '&': return new byte[] { 0x30, 0x7A, 0x4F, 0x5D, 0x37, 0x7A, 0x48, 0x00 };
                case '\'': return new byte[] { 0x04, 0x07, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00 };
                case '(': return new byte[] { 0x00, 0x1C, 0x3E, 0x63, 0x41, 0x00, 0x00, 0x00 };
                case ')': return new byte[] { 0x00, 0x41, 0x63, 0x3E, 0x1C, 0x00, 0x00, 0x00 };
                case '*': return new byte[] { 0x08, 0x2A, 0x3E, 0x1C, 0x1C, 0x3E, 0x2A, 0x08 };
                case '+': return new byte[] { 0x08, 0x08, 0x3E, 0x3E, 0x08, 0x08, 0x00, 0x00 };
                case ',': return new byte[] { 0x00, 0x80, 0xE0, 0x60, 0x00, 0x00, 0x00, 0x00 };
                case '-': return new byte[] { 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x00, 0x00 };
                case '.': return new byte[] { 0x00, 0x00, 0x60, 0x60, 0x00, 0x00, 0x00, 0x00 };
                case '/': return new byte[] { 0x60, 0x30, 0x18, 0x0C, 0x06, 0x03, 0x01, 0x00 };
                case '0': return new byte[] { 0x3E, 0x7F, 0x71, 0x59, 0x4D, 0x7F, 0x3E, 0x00 };
                case '1': return new byte[] { 0x40, 0x42, 0x7F, 0x7F, 0x40, 0x40, 0x00, 0x00 };
                case '2': return new byte[] { 0x62, 0x73, 0x59, 0x49, 0x6F, 0x66, 0x00, 0x00 };
                case '3': return new byte[] { 0x22, 0x63, 0x49, 0x49, 0x7F, 0x36, 0x00, 0x00 };
                case '4': return new byte[] { 0x18, 0x1C, 0x16, 0x53, 0x7F, 0x7F, 0x50, 0x00 };
                case '5': return new byte[] { 0x27, 0x67, 0x45, 0x45, 0x7D, 0x39, 0x00, 0x00 };
                case '6': return new byte[] { 0x3C, 0x7E, 0x4B, 0x49, 0x79, 0x30, 0x00, 0x00 };
                case '7': return new byte[] { 0x03, 0x03, 0x71, 0x79, 0x0F, 0x07, 0x00, 0x00 };
                case '8': return new byte[] { 0x36, 0x7F, 0x49, 0x49, 0x7F, 0x36, 0x00, 0x00 };
                case '9': return new byte[] { 0x06, 0x4F, 0x49, 0x69, 0x3F, 0x1E, 0x00, 0x00 };
                case ':': return new byte[] { 0x00, 0x00, 0x66, 0x66, 0x00, 0x00, 0x00, 0x00 };
                case ';': return new byte[] { 0x00, 0x80, 0xE6, 0x66, 0x00, 0x00, 0x00, 0x00 };
                case '<': return new byte[] { 0x08, 0x1C, 0x36, 0x63, 0x41, 0x00, 0x00, 0x00 };
                case '=': return new byte[] { 0x24, 0x24, 0x24, 0x24, 0x24, 0x24, 0x00, 0x00 };
                case '>': return new byte[] { 0x00, 0x41, 0x63, 0x36, 0x1C, 0x08, 0x00, 0x00 };
                case '?': return new byte[] { 0x02, 0x03, 0x51, 0x59, 0x0F, 0x06, 0x00, 0x00 };
                case '@': return new byte[] { 0x3E, 0x7F, 0x41, 0x5D, 0x5D, 0x1F, 0x1E, 0x00 };
                case 'A': return new byte[] { 0x7C, 0x7E, 0x13, 0x13, 0x7E, 0x7C, 0x00, 0x00 };
                case 'B': return new byte[] { 0x41, 0x7F, 0x7F, 0x49, 0x49, 0x7F, 0x36, 0x00 };
                case 'C': return new byte[] { 0x1C, 0x3E, 0x63, 0x41, 0x41, 0x63, 0x22, 0x00 };
                case 'D': return new byte[] { 0x41, 0x7F, 0x7F, 0x41, 0x63, 0x3E, 0x1C, 0x00 };
                case 'E': return new byte[] { 0x41, 0x7F, 0x7F, 0x49, 0x5D, 0x41, 0x63, 0x00 };
                case 'F': return new byte[] { 0x41, 0x7F, 0x7F, 0x49, 0x1D, 0x01, 0x03, 0x00 };
                case 'G': return new byte[] { 0x1C, 0x3E, 0x63, 0x41, 0x51, 0x73, 0x72, 0x00 };
                case 'H': return new byte[] { 0x7F, 0x7F, 0x08, 0x08, 0x7F, 0x7F, 0x00, 0x00 };
                case 'I': return new byte[] { 0x00, 0x41, 0x7F, 0x7F, 0x41, 0x00, 0x00, 0x00 };
                case 'J': return new byte[] { 0x30, 0x70, 0x40, 0x41, 0x7F, 0x3F, 0x01, 0x00 };
                case 'K': return new byte[] { 0x41, 0x7F, 0x7F, 0x08, 0x1C, 0x77, 0x63, 0x00 };
                case 'L': return new byte[] { 0x41, 0x7F, 0x7F, 0x41, 0x40, 0x60, 0x70, 0x00 };
                case 'M': return new byte[] { 0x7F, 0x7F, 0x0E, 0x1C, 0x0E, 0x7F, 0x7F, 0x00 };
                case 'N': return new byte[] { 0x7F, 0x7F, 0x06, 0x0C, 0x18, 0x7F, 0x7F, 0x00 };
                case 'O': return new byte[] { 0x1C, 0x3E, 0x63, 0x41, 0x63, 0x3E, 0x1C, 0x00 };
                case 'P': return new byte[] { 0x41, 0x7F, 0x7F, 0x49, 0x09, 0x0F, 0x06, 0x00 };
                case 'Q': return new byte[] { 0x1E, 0x3F, 0x21, 0x71, 0x7F, 0x5E, 0x00, 0x00 };
                case 'R': return new byte[] { 0x41, 0x7F, 0x7F, 0x09, 0x19, 0x7F, 0x66, 0x00 };
                case 'S': return new byte[] { 0x26, 0x6F, 0x4D, 0x59, 0x73, 0x32, 0x00, 0x00 };
                case 'T': return new byte[] { 0x03, 0x41, 0x7F, 0x7F, 0x41, 0x03, 0x00, 0x00 };
                case 'U': return new byte[] { 0x7F, 0x7F, 0x40, 0x40, 0x7F, 0x7F, 0x00, 0x00 };
                case 'V': return new byte[] { 0x1F, 0x3F, 0x60, 0x60, 0x3F, 0x1F, 0x00, 0x00 };
                case 'W': return new byte[] { 0x7F, 0x7F, 0x30, 0x18, 0x30, 0x7F, 0x7F, 0x00 };
                case 'X': return new byte[] { 0x43, 0x67, 0x3C, 0x18, 0x3C, 0x67, 0x43, 0x00 };
                case 'Y': return new byte[] { 0x07, 0x4F, 0x78, 0x78, 0x4F, 0x07, 0x00, 0x00 };
                case 'Z': return new byte[] { 0x47, 0x63, 0x71, 0x59, 0x4D, 0x67, 0x73, 0x00 };
                case '[': return new byte[] { 0x00, 0x7F, 0x7F, 0x41, 0x41, 0x00, 0x00, 0x00 };
                case '\\': return new byte[] { 0x01, 0x03, 0x06, 0x0C, 0x18, 0x30, 0x60, 0x00 };
                case ']': return new byte[] { 0x00, 0x41, 0x41, 0x7F, 0x7F, 0x00, 0x00, 0x00 };
                case '^': return new byte[] { 0x08, 0x0C, 0x06, 0x03, 0x06, 0x0C, 0x08, 0x00 };
                case '_': return new byte[] { 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80 };
                case '`': return new byte[] { 0x00, 0x00, 0x03, 0x07, 0x04, 0x00, 0x00, 0x00 };
                case 'a': return new byte[] { 0x20, 0x74, 0x54, 0x54, 0x3C, 0x78, 0x40, 0x00 };
                case 'b': return new byte[] { 0x41, 0x7F, 0x3F, 0x48, 0x48, 0x78, 0x30, 0x00 };
                case 'c': return new byte[] { 0x38, 0x7C, 0x44, 0x44, 0x6C, 0x28, 0x00, 0x00 };
                case 'd': return new byte[] { 0x30, 0x78, 0x48, 0x49, 0x3F, 0x7F, 0x40, 0x00 };
                case 'e': return new byte[] { 0x38, 0x7C, 0x54, 0x54, 0x5C, 0x18, 0x00, 0x00 };
                case 'f': return new byte[] { 0x48, 0x7E, 0x7F, 0x49, 0x03, 0x02, 0x00, 0x00 };
                case 'g': return new byte[] { 0x98, 0xBC, 0xA4, 0xA4, 0xF8, 0x7C, 0x04, 0x00 };
                case 'h': return new byte[] { 0x41, 0x7F, 0x7F, 0x08, 0x04, 0x7C, 0x78, 0x00 };
                case 'i': return new byte[] { 0x00, 0x44, 0x7D, 0x7D, 0x40, 0x00, 0x00, 0x00 };
                case 'j': return new byte[] { 0x60, 0xE0, 0x80, 0x80, 0xFD, 0x7D, 0x00, 0x00 };
                case 'k': return new byte[] { 0x41, 0x7F, 0x7F, 0x10, 0x38, 0x6C, 0x44, 0x00 };
                case 'l': return new byte[] { 0x00, 0x41, 0x7F, 0x7F, 0x40, 0x00, 0x00, 0x00 };
                case 'm': return new byte[] { 0x7C, 0x7C, 0x18, 0x38, 0x1C, 0x7C, 0x78, 0x00 };
                case 'n': return new byte[] { 0x7C, 0x7C, 0x04, 0x04, 0x7C, 0x78, 0x00, 0x00 };
                case 'o': return new byte[] { 0x38, 0x7C, 0x44, 0x44, 0x7C, 0x38, 0x00, 0x00 };
                case 'p': return new byte[] { 0x84, 0xFC, 0xF8, 0xA4, 0x24, 0x3C, 0x18, 0x00 };
                case 'q': return new byte[] { 0x18, 0x3C, 0x24, 0xA4, 0xF8, 0xFC, 0x84, 0x00 };
                case 'r': return new byte[] { 0x44, 0x7C, 0x78, 0x4C, 0x04, 0x1C, 0x18, 0x00 };
                case 's': return new byte[] { 0x48, 0x5C, 0x54, 0x54, 0x74, 0x24, 0x00, 0x00 };
                case 't': return new byte[] { 0x00, 0x04, 0x3E, 0x7F, 0x44, 0x24, 0x00, 0x00 };
                case 'u': return new byte[] { 0x3C, 0x7C, 0x40, 0x40, 0x3C, 0x7C, 0x40, 0x00 };
                case 'v': return new byte[] { 0x1C, 0x3C, 0x60, 0x60, 0x3C, 0x1C, 0x00, 0x00 };
                case 'w': return new byte[] { 0x3C, 0x7C, 0x70, 0x38, 0x70, 0x7C, 0x3C, 0x00 };
                case 'x': return new byte[] { 0x44, 0x6C, 0x38, 0x10, 0x38, 0x6C, 0x44, 0x00 };
                case 'y': return new byte[] { 0x9C, 0xBC, 0xA0, 0xA0, 0xFC, 0x7C, 0x00, 0x00 };
                case 'z': return new byte[] { 0x4C, 0x64, 0x74, 0x5C, 0x4C, 0x64, 0x00, 0x00 };
                case '{': return new byte[] { 0x08, 0x08, 0x3E, 0x77, 0x41, 0x41, 0x00, 0x00 };
                case '|': return new byte[] { 0x00, 0x00, 0x00, 0x77, 0x77, 0x00, 0x00, 0x00 };
                case '}': return new byte[] { 0x41, 0x41, 0x77, 0x3E, 0x08, 0x08, 0x00, 0x00 };
                case '~': return new byte[] { 0x02, 0x03, 0x01, 0x03, 0x02, 0x03, 0x01, 0x00 };
                default:
                    return new byte[] { };
            }
        }
    }
}
