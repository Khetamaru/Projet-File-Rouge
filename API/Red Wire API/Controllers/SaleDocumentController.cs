using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Local_API_Server.Models;
using Projet_File_Rouge.Object;
using System;

namespace Local_API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDocumentController : ControllerBase
    {
        private readonly SaleDocumentContext _context;

        public SaleDocumentController(SaleDocumentContext context) { _context = context; }

        // GET: api/SaleDocument/5
        [HttpGet("{documentNumber}")]
        public async Task<ActionResult<SaleDocument>> GetSaleDocument(string documentNumber)
        {
            var SaleDocument = await _context.SaleDocument.Where(r => r.DocumentNumber == documentNumber).ToListAsync();
            if (SaleDocument == null)
            {
                return NotFound();
            }
            return SaleDocument[0];
        }
    }
}
