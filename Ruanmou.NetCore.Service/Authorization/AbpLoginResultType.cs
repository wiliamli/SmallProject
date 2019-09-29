using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Service.Authorization
{
    public enum LoginResultType : byte
    {
        Success = 1,
        InvalidUserNameOrEmailAddress = 2,
        InvalidPassword = 3,
        UserIsNotActive = 4,
        InvalidTenancyName = 5,
        TenantIsNotActive = 6,
        UserEmailIsNotConfirmed = 7,
        UnknownExternalLogin = 8,
        LockedOut = 9,
        UserPhoneNumberIsNotConfirmed = 10
    }
}
