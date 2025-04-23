using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace efCoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;


        // yapıcı metot.veritabanına eirşilmesi için kullanılır. 
        public OgrenciController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // Listeleme bölümü

        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler
                .OrderBy(o => o.OgrenciId) // Sıralamayı ID'ye göre yap
                .ToListAsync();

            return View(ogrenciler);
        }




        [HttpPost] // form gönderildiğinde bu çalışır yani post edğildiğinde. 
        public async Task<IActionResult> Create(Ogrenci model)
        {
            if (ModelState.IsValid) // boş değilse 
            {
                _context.Ogrenciler.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Başarılıysa anasayfaya yönlendir
            }

            return View(model); // Hata varsa formu geri göster
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci); // Formu doldurulmuş şekilde açar
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if (id != model.OgrenciId)
            {
                return BadRequest(); // ID uyuşmazlığı varsa
            }

            if (ModelState.IsValid)
            {
                _context.Ogrenciler.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model); // Hatalıysa formu tekrar göster
        }
    }
}
