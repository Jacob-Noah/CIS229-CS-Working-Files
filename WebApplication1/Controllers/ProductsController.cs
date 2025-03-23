using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccessLayer;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductsController : Controller
{
    // Main products list view
    public ActionResult Index()
    {
        ProductDataAccess dataAccess = new ProductDataAccess();
        List<ProductModel> productList = dataAccess.GetProductList();

        // Populate ViewData with messages from redirects if any
        ViewData["Message"] = TempData["Message"];
        ViewData["Error"] = TempData["Error"];
        
        return View(productList);
    }

    // Add product form page
    public ActionResult New()
    {
        return View("New");
    }

    // Add product
    [HttpPost]
    public ActionResult AddProduct(string name, int quantity, int customerId)
    {
        ProductDataAccess dataAccess = new ProductDataAccess();
        bool created = dataAccess.CreateProduct(name, quantity, customerId);

        if (created)
        {
            TempData["Message"] = "Product added successfully";
        }
        else
        {
            TempData["Error"] = "Failed to add product, make sure you are using a valid customer ID";
        }

        return RedirectToAction("Index");
    }
    
    // Update product form page
    // I'm using the model itself to populate form with the existing data to update
    public ActionResult Update()
    {
        int productId = int.Parse(RouteData.Values["id"].ToString());
        ProductDataAccess dataAccess = new ProductDataAccess();

        ProductModel product = dataAccess.GetProduct(productId);

        return View(product);
    }
    
    // Update product
    [HttpPost]
    public ActionResult UpdateProduct(int id, string name, int quantity, int customerId)
    {
        ProductDataAccess dataAccess = new ProductDataAccess();
        bool updated = dataAccess.UpdateProduct(id, name, quantity, customerId);

        if (updated)
        {
            TempData["Message"] = "Product updated successfully";
        }
        else
        {
            TempData["Error"] = "Failed to update product, check your inputs and try again";
        }

        return RedirectToAction("Index");
    }

    // Delete product
    public ActionResult Delete(int id)
    {
        int productId = int.Parse(RouteData.Values["id"].ToString());

        ProductDataAccess dataAccess = new ProductDataAccess();
        bool deleted = dataAccess.DeleteProduct(productId);
        if (deleted)
        {
            TempData["Message"] = "Product deleted successfully";
        }
        else
        {
            TempData["Message"] = "Failed to delete product, check your inputs and try again";
        }

        return RedirectToAction("Index");
    }
}