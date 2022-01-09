using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheLivery.Data;
using TheLivery.Models;

namespace TheLivery.Pages.Comenzi
{
    public class CreateModel : PageModel
    {
        private readonly TheLivery.Data.CurieratContext _context;

        public CreateModel(TheLivery.Data.CurieratContext context)
        {
            _context = context;
        }

        public IList<Comanda> Comenzi { get; set; }
        public IList<Colet> Colete { get; set; }
        public IList<Curier> Curieri { get; set; }

        public IActionResult OnGet()
        {
            Comenzi = _context.Comenzi
                .Include(c => c.Colet)
                .Include(c => c.Curier).ToList();
            Colete = _context.Colete
                .Include(c => c.Client)
                .Include(c => c.Firma).ToList();
            Curieri = _context.Curieri.ToList();
            ViewData["ColetID"] = new SelectList(_context.Set<Colet>(), "ID", "ID");
            ViewData["CurierID"] = new SelectList(_context.Set<Curier>(), "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Comanda Comanda { get; set; }
        //public string Message { get; set; } = "hAIIII";

        /*public async Task<IActionResult> OnPostAsync()
        {
            var x = Request.Form["id"];
            Console.WriteLine(x);
            Message = "HAIDA BAAAAAAAAAAAAAAAA" + Request.Form["id"];

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Comenzi.Add(Comanda);
            await _context.SaveChangesAsync();

            return RedirectToPage("");
        }*/

        private string RandomString()
        {
            Random RNG = new Random();
            int length = 10;
            var rString = "";
            for (var i = 0; i < length; i++)
            {
                rString += ((char)(RNG.Next(1, 26) + 64)).ToString().ToLower();
            }
            return rString;
        }

        public Colet Colet { get; set; }

        public void OnPost()
        {
             Comenzi = _context.Comenzi
                .Include(c => c.Colet)
                .Include(c => c.Curier).ToList();
                Colete = _context.Colete
                    .Include(c => c.Client)
                    .Include(c => c.Firma).ToList();
                Curieri = _context.Curieri.ToList();                
                ViewData["ColetID"] = new SelectList(_context.Set<Colet>(), "ID", "ID");
                ViewData["CurierID"] = new SelectList(_context.Set<Curier>(), "ID", "ID");
            var stareComanda = Request.Form["stareComanda"];
            if(stareComanda.Equals("LIVRAT"))
            {
                Comanda = _context.Comenzi
                .Include(c => c.Colet)
                .Include(c => c.Curier).FirstOrDefault(m => m.ID.Equals(int.Parse(Request.Form["idComanda"])));
            }
            else
            {
               

                Comanda = new Comanda {CurierID =  int.Parse(Request.Form["idcurier"]), ColetID = int.Parse(Request.Form["id"]), Stare = "In tranzit", Data_livrare =  new DateTime(),AWB = RandomString() };
                _context.Comenzi.Add(Comanda);

                Colet = _context.Colete
                    .Include(c => c.Client)
                    .Include(c => c.Firma).FirstOrDefault(m => m.ID.Equals(int.Parse(Request.Form["id"])));
                Colet.Stare = "Preluat";
                _context.Attach(Colet).State = EntityState.Modified;
            }
            //Colete.Where(x => x.ID.Equals(Request.Form["id"])).First().Stare = "Preluat";
            _context.SaveChangesAsync();
        }
    }
}
