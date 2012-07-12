using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jiyuu.Aggregation.Common.Enums
{
    public enum FeedTypeEnum:byte
    { 
        RSS2=1,
        ATOM=2
    }
    public enum FeedReqTypeEnum : byte
    {
        Posts=1,
        Comments=2
    }
}
