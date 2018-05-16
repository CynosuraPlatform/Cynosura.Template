using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cynosura.Template.Core.Security
{
    public interface IUserInfoProvider
    {
        Task<UserInfoModel> GetUserInfoAsync();
    }
}
