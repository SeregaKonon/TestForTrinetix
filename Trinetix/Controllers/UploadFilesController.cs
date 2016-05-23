using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Trinetix.Models;

namespace Trinetix.Controllers
{
    
    public class UploadFilesController : Controller
    {


        DbFileContext db = new DbFileContext();
        // GET: UploadFiles
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string folder)
        {
            string searchCatalog = folder;
            try
            { 
            
            string[] filespatharray = System.IO.Directory.GetFiles(searchCatalog, "*.txt");
            foreach (string filepath in filespatharray)
                {
                    var result = System.IO.File.ReadAllLines(filepath, Encoding.Default)
                        .AsParallel()
                        .Select(s => WorkWithTxt.DevideByWords(s))
                        .ToList();

                    FileTypes ft = WorkWithTxt.AddFileTypes(filepath);
                    Dirrectories dir = WorkWithTxt.AddDirrectories(filepath);
                    Files f = WorkWithTxt.AddFiles(filepath, ft, dir);
                    WorkWithTxt.AddFilesData(filepath, f);
                    List<Words> wordList = WorkWithTxt.AddWords(result, f);
                    WorkWithTxt.BulkInserting(wordList);

                }
            }
            catch { }

            return RedirectToAction("Index", "Words");

        }

        
    }
}