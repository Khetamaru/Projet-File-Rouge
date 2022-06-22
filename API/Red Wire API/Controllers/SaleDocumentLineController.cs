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
    public class SaleDocumentLineController : ControllerBase
    {
        private readonly SaleDocumentLineContext _context;

        public SaleDocumentLineController(SaleDocumentLineContext context) { _context = context; }

        // GET: api/SaleDocumentLine/5
        [HttpGet("{documentId}")]
        public async Task<ActionResult<IEnumerable<SaleDocumentLine>>> GetSaleDocumentLine(string documentId)
        {
            var SaleDocumentLine = await _context.SaleDocumentLine.Where(r => r.DocumentId == Guid.Parse(documentId)).ToListAsync();
            if (SaleDocumentLine == null)
            {
                return NotFound();
            }
            return SaleDocumentLine;
        }
    }
}
