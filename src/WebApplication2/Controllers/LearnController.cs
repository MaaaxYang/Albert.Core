using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Options;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LearnController : Controller
    {
        private readonly Learn _learn;
        public LearnController(IOptions<Learn> learn)
        {
            _learn = learn.Value;
        }

        public object Index()
        {
            return 123;
        }

        public object Test()
        {
            return _learn;
        }
    }

}