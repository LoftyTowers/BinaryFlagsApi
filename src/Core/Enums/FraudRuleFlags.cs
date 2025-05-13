using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Enums;

[Flags]
public enum FraudRuleFlags
{
    None    = 0,
    Rule1   = 1 << 0, // 1
    Rule2   = 1 << 1, // 2
    Rule3   = 1 << 2, // 4
    Rule4   = 1 << 3, // 8
    Rule5   = 1 << 4, // 16
    Rule6   = 1 << 5, // 32
    Rule7   = 1 << 6, // 64
    Rule8   = 1 << 7, // 128
    Rule9   = 1 << 8, // 256
    Rule10  = 1 << 9  // 512
}
