using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsMakeFast
{
    static class Res
    {
        public static string configTemplate = @"
# the Added speed (saves the data from the option menu)
addedspeed = {addedspeed}

# The amount of speed Added per item
speedperitem = {speedperitem}

# the key used to show your current game speed
speedkey = {speedkey}

# -----All Possible Keys---------
# Alpha0, Alpha1, Alpha2, Alpha3, Alpha4
# Alpha5, Alpha6, Alpha7, Alpha8, Alpha9
# Backspace, Tab, Clear, Return, Pause
# Escape, Space, Exclaim, DoubleQuote, Hash
# Dollar, Percent, Ampersand, Quote, LeftParen
# RightParen, Asterisk, Plus, Comma, Minus
# Period, Slash, Colon, Semicolon, Less
# Equals, Greater, Question, At, LeftBracket
# Backslash, RightBracket, Caret, Underscore, BackQuote
# A, B, C, D, E
# F, G, H, I, J
# K, L, M, N, O
# P, Q, R, S, T
# U, V, W, X, Y
# Z, Delete, Keypad0, Keypad1, Keypad2
# Keypad3, Keypad4, Keypad5, Keypad6, Keypad7
# Keypad8, Keypad9, KeypadPeriod, KeypadDivide, KeypadMultiply
# KeypadMinus, KeypadPlus, KeypadEnter, KeypadEquals, UpArrow
# DownArrow, RightArrow, LeftArrow, Insert, Home
# End, PageUp, PageDown, F1, F2
# F3, F4, F5, F6, F7
# F8, F9, F10, F11, F12
# F13, F14, F15, Numlock, CapsLock
# ScrollLock, RightShift, LeftShift, RightControl, LeftControl
# RightAlt, LeftAlt, LeftCommand, LeftApple, LeftWindows
# RightCommand, RightApple, RightWindows, AltGr, Help
# Print, SysReq, Break, Menu
        ";


        public static string LocaFloat(string key) 
        {
            if (float.TryParse(key, out float parsed))
            {
                return key;
            }
            else if (float.TryParse(key.Replace('.', ','), out  parsed))
            {
                return key.Replace('.', ',');
            }
            else if (float.TryParse(key.Replace(',', '.'), out parsed))
            {
                return key.Replace(',', '.');
            }
            return null;
        }
    }
}
