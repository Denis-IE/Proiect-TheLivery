﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheLivery.Data;
using TheLivery.Models;

namespace TheLivery.Pages.Colete
{
    public class DetailsModel : PageModel
    {
        private readonly TheLivery.Data.CurieratContext _context;

        public DetailsModel(TheLivery.Data.CurieratContext context)
        {
            _context = context;
        }

        public Colet Colet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Colet = await _context.Colete
                .Include(c => c.Client)
                .Include(c => c.Firma).FirstOrDefaultAsync(m => m.ID == id);

            if (Colet == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
