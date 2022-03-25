﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationFoodForHumanRace.Models.Json
{
    public class AuthSignInResponse
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}