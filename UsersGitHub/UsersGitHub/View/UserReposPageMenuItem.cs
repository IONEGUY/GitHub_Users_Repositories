﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersGitHub.View
{

    public class UserReposPageMenuItem
    {
        public UserReposPageMenuItem()
        {
            TargetType = typeof(UserReposPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}