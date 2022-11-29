using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Brotherhood_Server.Data;
using Brotherhood_Server.Models;

namespace Brotherhood_Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class ContractTargetsController : ControllerBase
    {
        private readonly BrotherhoodServerContext _context;

        public ContractTargetsController(BrotherhoodServerContext context)
        {
            _context = context;
        }

    }
}
