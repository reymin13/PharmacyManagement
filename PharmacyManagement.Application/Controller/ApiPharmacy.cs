using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Application.Services;
using PharmacyManagement.Application.data;
using PharmacyManagement.Application.Models;
using System.Data.Common;
//using PharmacyManagement.Application.ModelRequests;

namespace PharmacyManagement.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPharmacy : ControllerBase
    {
        private PharmacyDbContext _db;
        public ApiPharmacy(PharmacyDbContext db)
        {
            _db = db;
        }




        [HttpGet("Categories")]
        public IEnumerable<Category> GetCategories()
        {

            IEnumerable<Category> Categories = _db.Categories.ToArray();
            return Categories;
        }

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category data is required.");
            }

            _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            var products = _db.Products
                .Select(p => new
                {
                    id = p.ProductId,
                    name = p.Name,
                    quantity = p.Quantity,
                    price = p.Price,
                    batchNumber = p.BatchNumber,
                    category = new { id = p.CategoryId, name = p.CategoryNavigation.Name }
                })
                .ToList();

            return Ok(products);
        }

        [HttpPost("products")]
        public async Task<IActionResult> AddProduct([FromBody] Product productreq)
        {
            if (productreq == null)
            {
                return BadRequest(new { message = "Produktdaten sind erforderlich." });
            }

            Product product = new Product();
            int maxId = _db.Products.ToArray().OrderByDescending(c => c.ProductId).FirstOrDefault()?.ProductId ?? 0;
            product.ProductId = maxId + 1;
            product.Name = productreq.Name;
            product.Quantity = productreq.Quantity;
            product.Description = productreq.Description;
            product.Category = productreq.Category;
            product.BatchNumber = productreq.BatchNumber;
            product.ExpireDate = productreq.ExpireDate;
            product.Manufacturer = productreq.Manufacturer;
            product.CategoryId = productreq.CategoryId;
            product.Price = productreq.Price;

            var existingCategory = await _db.Categories.FindAsync(product.CategoryId);

            if (existingCategory == null)
            {
                return BadRequest(new { message = "Kategorie nicht gefunden." });
            }

            product.CategoryNavigation = existingCategory;
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducts), new { id = product.ProductId }, new
            {
                id = product.ProductId,
                name = product.Name,
                quantity = product.Quantity,
                price = product.Price,
                category = new { id = existingCategory.Id, name = existingCategory.Name }
            });
        }

        [HttpPost("employees")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeereq)
        {
            if (employeereq == null)
            {
                return BadRequest(new { message = "Employee data is required." });
            }
            Employee employee = new Employee();
            int maxId = _db.Employees.ToArray().OrderByDescending(c => c.EmployeeId).FirstOrDefault()?.EmployeeId ?? 0;
            employee.EmployeeId = maxId + 1;
            employee.FirstName = employeereq.FirstName;
            employee.LastName = employeereq.LastName;
            employee.Username = employeereq.Username;
            employee.PasswordHash = employeereq.PasswordHash;
            employee.Role = employeereq.Role;
            employee.StorageId = employeereq.StorageId;


            // Ensure username is unique
            var existingEmployee = await _db.Employees.FirstOrDefaultAsync(e => e.Username == employee.Username);
            if (existingEmployee != null)
            {
                return BadRequest(new { message = "Username already exists." });
            }

            // Validate StorageId if provided
            if (employee.StorageId.HasValue)
            {
                var storageExists = await _db.Storages.AnyAsync(s => s.StorageId == employee.StorageId.Value);
                if (!storageExists)
                {
                    return BadRequest(new { message = "Storage not found." });
                }
            }

            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(AddEmployee), new { id = employee.EmployeeId }, employee);
        }

        [HttpPost("payments")]
        public async Task<IActionResult> AddPayment([FromBody] Payment paymentreq)
        {
            if (paymentreq == null)
            {
                return BadRequest(new { message = "Payment data is required." });
            }

            Payment payment = new Payment();
            int maxId = _db.Payments.ToArray().OrderByDescending(c => c.PaymentId).FirstOrDefault()?.PaymentId ?? 0;
            payment.PaymentId = maxId + 1;
            payment.Amount = paymentreq.Amount;
            payment.Method = paymentreq.Method;
            payment.InsurancePayment = paymentreq.InsurancePayment;
            payment.SaleId = paymentreq.SaleId;
            payment.InsuranceProviderId = paymentreq.InsuranceProviderId;
            var existingSale = await _db.Sales.FindAsync(payment.SaleId);
            if (existingSale == null)
            {
                return BadRequest(new { message = "Sale not found." });
            }

            payment.Sale = existingSale;
            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetpaymentsById), new { id = payment.PaymentId }, payment);
        }

        [HttpPost("confirmedSales")]
        public async Task<IActionResult> AddConfirmedSale([FromBody] ConfirmedSale confirmedSalereq)
        {
            if (confirmedSalereq == null)
            {
                return BadRequest(new { message = "confirmedSalereq data is required." });
            }

            //adding to confimed sale

            ConfirmedSale confirmedSale = new ConfirmedSale();
            int ConfrimedSaleId = _db.ConfirmedSales.ToArray().OrderByDescending(c => c.Id).FirstOrDefault()?.Id ?? 0;
            confirmedSale.Id = ConfrimedSaleId + 1;
            confirmedSale.ConfirmedDate = DateTime.UtcNow;
            confirmedSale.SaleId = confirmedSalereq.SaleId;
            confirmedSale.ProductId = confirmedSalereq.ProductId;
            confirmedSale.Paid = confirmedSalereq.Paid;

            var existingSale = await _db.Sales.FindAsync(confirmedSale.SaleId);
            if (existingSale == null)
            {
                return BadRequest(new { message = "Sale not found." });
            }

            confirmedSale.Sale = existingSale;
            _db.ConfirmedSales.Add(confirmedSale);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(AddConfirmedSale), new { id = confirmedSale.Id }, confirmedSale);

        }

        [HttpGet("payments/{id}")]
        public async Task<IActionResult> GetpaymentsById(int id)
        {
            var payment = await _db.Payments.FindAsync(id);

            if (payment == null)
                return NotFound(new { message = "payment not found" });

            return Ok(payment);
        }

        [HttpPost("prescriptions")]
        public async Task<IActionResult> AddPrescription([FromBody] Prescription prescriptionReq)
        {
            if (prescriptionReq == null)
            {
                return BadRequest(new { message = "Prescription data is required." });
            }

            Prescription prescription = new Prescription();
            int maxId = _db.Prescriptions.ToArray().OrderByDescending(c => c.Id).FirstOrDefault()?.Id ?? 0;
            prescription.Id = maxId + 1;
            prescription.IssueDate = prescriptionReq.IssueDate;
            prescription.DoctorName = prescriptionReq.DoctorName;
            prescription.CustomerId = prescriptionReq.CustomerId;
            prescription.ProductId = prescriptionReq.ProductId;
            prescription.IsUsed = false;


            var customer = await _db.Customers.FindAsync(prescription.CustomerId);
            var product = await _db.Products.FindAsync(prescription.ProductId);

            if (customer == null)
            {
                return BadRequest(new { message = "Customer not found." });
            }

            if (product == null)
            {
                return BadRequest(new { message = "Product not found." });
            }

            _db.Prescriptions.Add(prescription);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(AddPrescription), new { id = prescription.Id }, prescription);
        }

        [HttpPost("sales")]
        public async Task<IActionResult> AddSale([FromBody] Sale saleReq)
        {
            if (saleReq == null)
            {
                return BadRequest(new { message = "Sale data is required." });
            }
            int maxId = _db.Sales.ToArray().OrderByDescending(c => c.SaleId).FirstOrDefault()?.SaleId ?? 0;
            Sale sale = new Sale();
            sale.SaleId = maxId + 1;
            sale.SaleDate = DateTime.UtcNow;
            sale.TotalAmount = saleReq.TotalAmount;
            sale.EmployeeId = saleReq.EmployeeId;
            sale.CustomerId = saleReq.CustomerId;
            sale.Quantity = saleReq.Quantity;
            sale.Discount = saleReq.Discount;

            var employee = await _db.Employees.FindAsync(sale.EmployeeId);
            var customer = await _db.Customers.FindAsync(sale.CustomerId);

            if (employee == null)
            {
                return BadRequest(new { message = "Employee not found." });
            }

            if (customer == null)
            {
                return BadRequest(new { message = "Customer not found." });
            }

            _db.Sales.Add(sale);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(AddSale), new { id = sale.SaleId }, sale);
        }

        [HttpPost("storages")]
        public async Task<IActionResult> AddStorage([FromBody] Storage storageReq)
        {
            if (storageReq == null)
            {
                return BadRequest(new { message = "Storage data is required." });
            }

            Storage storage = new Storage();
            int maxId = _db.Storages.ToArray().OrderByDescending(c => c.StorageId).FirstOrDefault()?.StorageId ?? 0;
            storage.StorageId = maxId + 1;
            storage.CurrentStock = storageReq.CurrentStock;
            storage.MaxStock = storageReq.MaxStock;


            _db.Storages.Add(storage);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(AddStorage), new { id = storage.StorageId }, storage);
        }

        [HttpPost("customers")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customerReq)
        {
            if (customerReq == null)
            {
                return BadRequest(new { message = "Customer data is required." });
            }
            Customer customer = new Customer();
            int maxCustomerId = _db.Customers.ToArray().OrderByDescending(c => c.Id).FirstOrDefault()?.Id ?? 0;
            maxCustomerId++;
            customer.Id = maxCustomerId;
            customer.InsuranceNumber = customerReq.InsuranceNumber;
            customer.LastName = customerReq.LastName;
            customer.FirstName = customerReq.FirstName;
            customer.InsuranceProviderId = customerReq.InsuranceProviderId;
            customer.Contact = customerReq.Contact;
            customer.LoyaltyPoints = 0;

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(AddCustomer), new { id = customer.Id }, customer);
        }

        [HttpGet("customers")]
        public async Task<IActionResult> getCustomers()
        {
            var customers = await _db.Customers
             .Include(c => c.InsuranceProvider)
             .Select(c => new
             {
                 c.Id,
                 c.FirstName,
                 c.LastName,
                 c.InsuranceNumber,
                 c.Contact,
                 c.LoyaltyPoints,
                 InsuranceProvider = c.InsuranceProvider != null ? new { c.InsuranceProvider.Id, c.InsuranceProvider.Name } : null
             })
             .ToListAsync();

            return Ok(customers);
        }

        [HttpGet("insuranceProviders")]
        public async Task<IActionResult> GetInsuranceProviders()
        {
            var providers = await _db.InsuranceProviders.ToListAsync();
            return Ok(providers);
        }

        [HttpPost("insuranceProviders")]
        public async Task<IActionResult> AddInsuranceProvider([FromBody] InsuranceProvider provider)
        {
            if (provider == null)
                return BadRequest(new { message = "Insurance provider data is required." });
            int maxInsurranceId = _db.InsuranceProviders.ToArray().OrderByDescending(c => c.Id).FirstOrDefault()?.Id ?? 0;
            maxInsurranceId++;
            provider.Id = maxInsurranceId;
            _db.InsuranceProviders.Add(provider);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInsuranceProvider), new { id = provider.Id }, provider);
        }

        [HttpGet("insuranceProviders/{id}")]
        public async Task<IActionResult> GetInsuranceProvider(int id)
        {
            var provider = await _db.InsuranceProviders.FindAsync(id);

            if (provider == null)
                return NotFound(new { message = "Insurance provider not found" });

            return Ok(provider);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromBody] Login loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Username))
            {
                return BadRequest(new { message = "Username is required." });

            }
            if (string.IsNullOrEmpty(loginRequest.PasswordHash))
            {
                return BadRequest(new { message = "Passwordhash is required." });

            }

            // Find user by username
            var employee = await _db.Employees
                .FirstOrDefaultAsync(e => e.Username == loginRequest.Username);

            if (employee == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            // Check if password matches
            if (employee.PasswordHash != loginRequest.PasswordHash) // Consider hashing the password
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            HttpContext.Session.SetString(employee.Username, employee.Role);

            return Ok(new
            {
                employee.EmployeeId,
                employee.FirstName,
                employee.LastName,
                employee.Username,
                employee.Role
            });
        }

        [HttpGet("GetSessionByUsername/{username}")]
        public IActionResult GetSessionByUsername(string username)
        {
            // Retrieve the session username
            var Role = HttpContext.Session.GetString(username);

            // Check if the session exists and matches the requested username
            if (string.IsNullOrEmpty(Role))
            {
                return Unauthorized(new { message = "Session not found or does not match the given username." });
            }

            // Retrieve other session values


            return Ok(new
            {
                username,
                Role
            });
        }










    }
}
