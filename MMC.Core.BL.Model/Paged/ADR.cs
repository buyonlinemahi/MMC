﻿using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class ADR : BasePaged
    {
        public IEnumerable<BLModel.ADR> ADRDetails { get; set; }
    }
}
