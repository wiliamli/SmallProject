﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ruanmou04.Core.Utility.Enums
{
    public enum UserStatus
    {
        [Description("禁用")]
        UNABLE_STATUS=0,

        [Description("可用")]
        ENABLE_STATUS = 1,

        [Description("删除")]
        DELETE_STATUS =-1

    }
}