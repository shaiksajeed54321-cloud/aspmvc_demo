using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SajidMvc.Data;
using SajidMvc.Models;

namespace SajidMvc.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _db;

    public ProductsController(AppDbContext db) => _db = db;

    // GET: /Products
    public async Task<IActionResult> Index(string? search)
    {
        var query = _db.Products.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search));

        ViewData["Search"] = search;
        return View(await query.OrderBy(p => p.Name).ToListAsync());
    }

    // GET: /Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? NotFound() : View(product);
    }

    // GET: /Products/Create
    public IActionResult Create() => View();

    // POST: /Products/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,Price,Stock")] Product product)
    {
        if (!ModelState.IsValid) return View(product);

        product.CreatedAt = DateTime.Now;
        _db.Add(product);
        await _db.SaveChangesAsync();
        TempData["Message"] = $"Product '{product.Name}' created.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var product = await _db.Products.FindAsync(id);
        return product == null ? NotFound() : View(product);
    }

    // POST: /Products/Edit/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Stock,CreatedAt")] Product product)
    {
        if (id != product.Id) return NotFound();
        if (!ModelState.IsValid) return View(product);

        try
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _db.Products.AnyAsync(p => p.Id == id)) return NotFound();
            throw;
        }

        TempData["Message"] = $"Product '{product.Name}' updated.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? NotFound() : View(product);
    }

    // POST: /Products/Delete/5
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            TempData["Message"] = $"Product '{product.Name}' deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
