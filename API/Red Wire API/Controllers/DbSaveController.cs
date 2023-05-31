using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Local_API_Server.Models;
using Projet_File_Rouge.Object;
using System.Diagnostics;
using System;
using System.IO;
using Newtonsoft.Json;
using Red_Wire_API;

namespace Local_API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbSaveController : ControllerBase
    {
        private readonly DbSaveContext _context;

        public DbSaveController(DbSaveContext context) { _context = context; }

        private bool DbSaveExists(long id) { return _context.DbSave.Any(e => e.id == id); }

        // GET: api/DbSave
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbSave>>> GetDbSave()
        {
            return await _context.DbSave.ToListAsync();
        }

        // GET: api/DbSave/save
        [HttpGet("save")]
        public async Task<ActionResult<string>> SaveDataBase()
        {
            string path = Directory.GetCurrentDirectory();
            string letter = path.Split(":")[0] + ":";
            string docPath = @"C:/dump_bdd.sql";

            Setup setup = SetupCacheFile.Start();

            RunCommand(new string[]
            {
                @"C:",
                @"cd C:\Program Files\MySQL\MySQL Server 8.0\bin",
                @"mysqldump -u " + setup.USER_NAME_LOCAL + " -p" + setup.PASSWORD_LOCAL + " " + setup.BDD_NAME_LOCAL + " > " + docPath,
                @letter,
                @"cd " + @path
            });

            await Task.Delay(1000);
            string[] text = System.IO.File.ReadAllLines(docPath);
            System.IO.File.Delete(docPath);

            DbSave DbSave = new DbSave { date = DateTime.Now };
            _context.DbSave.Add(DbSave);
            DbSave.PostChild(_context);
            await _context.SaveChangesAsync();

            CreatedAtAction(nameof(GetDbSave), new { id = DbSave.id }, DbSave);

            return JsonConvert.SerializeObject(text);
        }

        public static void RunCommand(string[] arguments)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe", "/user:Administrator")
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                Verb = "runas"
            };

            p.StartInfo = info;
            p.Start();

            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    foreach (string arg in arguments)
                    {
                        sw.WriteLine(arg);
                    }
                }
            }
        }

        // GET: api/DbSave/last
        [HttpGet("last")]
        public async Task<ActionResult<DbSave>> GetDbSaveLast()
        {
            DbSave DbSave = (await _context.DbSave.Where(d => d.date == _context.DbSave.Max(db => db.date)).ToListAsync()).First();
            if (DbSave == null)
            {
                return NotFound();
            }
            return DbSave;
        }
    }
}
