using Microsoft.AspNetCore.Mvc;
using RealEstateManagementSystem.Data;
using RealEstateManagementSystem.Models;

namespace RealEstateManagementSystem.Controllers
{
    public class PropertyController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment environment;

        public PropertyController(AppDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var property = context.RealEstateProperty.OrderByDescending(p => p.Id).ToList();
            return View(property);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RealEstatePropertyDto propertyDto)
        {
            if (propertyDto.ImageFile == null) {
                ModelState.AddModelError("ImageFile", "The image file is required");
            }

            if (!ModelState.IsValid) { 
                return View(propertyDto);
            }

            string newFileName = propertyDto.Name.ToLower().Replace(" ", "_");
            newFileName += Path.GetExtension(propertyDto.ImageFile.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath)) 
            { 
                propertyDto.ImageFile.CopyTo(stream);
            }

            RealEstateProperty realEstateProperty = new RealEstateProperty()
            {
                Name = propertyDto.Name,
                Description = propertyDto.Description,
                Type = propertyDto.Type,
                Price = propertyDto.Price,
                Image = newFileName,
                CreatedDate = DateTime.Now
            };

            context.RealEstateProperty.Add(realEstateProperty);
            context.SaveChanges();

            return RedirectToAction("Index", "Property");
        }

        public IActionResult Edit(int id) { 
            var property = context.RealEstateProperty.Find(id);

            if (property == null) 
            { 
                return RedirectToAction("Index", "Property");
            }

            var propertyDb = new RealEstatePropertyDto()
            {
                Name = property.Name,
                Description = property.Description,
                Type = property.Type,
                Price = property.Price,
            };

            ViewData["id"] = property.Id;
            ViewData["Image"] = property.Image;
            ViewData["Created Date"] = property.CreatedDate.ToString("MM/dd/yyyy");

            return View(propertyDb);
        }

        [HttpPost]
        public IActionResult Edit(int id, RealEstatePropertyDto propertyDto) {
            var property = context.RealEstateProperty.Find(id);

            if (property == null)
            {
                return RedirectToAction("Index", "Property");
            }

            if (ModelState.IsValid)
            {
                ViewData["id"] = property.Id;
                ViewData["Image"] = property.Image;
                ViewData["Created Date"] = property.CreatedDate.ToString("MM/dd/yyyy");

                return View(propertyDto);
            }

            string newFileName = propertyDto.Name.ToLower().Replace(" ", "_");
            newFileName += Path.GetExtension(propertyDto.ImageFile.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                propertyDto.ImageFile.CopyTo(stream);
            }

            string oldImageFullPath = environment.WebRootPath + "/images/" + newFileName;
            System.IO.File.Delete(imageFullPath);

            property.Name = propertyDto.Name;
            property.Description = propertyDto.Description;
            property.Type = propertyDto.Type;
            property.Price = propertyDto.Price;

            context.SaveChanges();


            return View(propertyDto);
        }

        public IActionResult Delete(int id)
        {
            var property = context.RealEstateProperty.Find(id);

            if (property == null)
            {
                return RedirectToAction("Index", "Property");
            }

            string imageFullPath = environment.WebRootPath + "/images/" + property.Image;
            System.IO.File.Delete(imageFullPath);

            context.RealEstateProperty.Remove(property);
            context.SaveChanges();

            return RedirectToAction("Index", "Property");
        }
        
    }
}
