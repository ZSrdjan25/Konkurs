using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KandidatController : ControllerBase
    {
        private  ApplicationDbContext applicationDbContext;

        public KandidatController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }

        //GET: api/Kandidat
        [HttpGet]
        public async Task<List<Kandidat>> GET()
        {
            return await applicationDbContext.Kandidat.ToListAsync();
        }

        //POST: api/Kandidat
        [HttpPost]
        public async Task<ActionResult<Kandidat>> POST(Kandidat kandidat)
        {
            await applicationDbContext.AddAsync(kandidat);
            await applicationDbContext.SaveChangesAsync();
            return kandidat;
        }

        //POST: api/Kandidat
        [HttpPut]
        public async Task<ActionResult<Kandidat>> PUT (Kandidat kandidat)
        {
            if(kandidat.KandidatId != 0)
            {
                Kandidat kandidat1 = await applicationDbContext.Kandidat.SingleOrDefaultAsync(x => x.KandidatId == kandidat.KandidatId);
                kandidat1.Ime = kandidat.Ime;
                kandidat1.Prezime = kandidat.Prezime;
                kandidat1.JMBG = kandidat.JMBG;
                kandidat1.GodinaRodjenja = kandidat.GodinaRodjenja;
                kandidat1.Telefon = kandidat.Telefon;
                kandidat1.Napomena = kandidat.Napomena;
                kandidat1.KandidatZaposlen = kandidat.KandidatZaposlen;
                kandidat1.DatumIzmjenePodataka = kandidat.DatumIzmjenePodataka;

                await applicationDbContext.SaveChangesAsync();
                return kandidat1;
            }
            return new Kandidat();
        }

        //DELETE: api/Kandidat/{id}
        [HttpDelete("{id}")]
        public async Task DELETE(int id)
        {
            Kandidat kandidat = await applicationDbContext.Kandidat.SingleOrDefaultAsync(x => x.KandidatId == id);
            applicationDbContext.Kandidat.Remove(kandidat);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
