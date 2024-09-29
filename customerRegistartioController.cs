  public ActionResult CustomerRegister()
  {
      return View();
  }
  [HttpPost]

 public ActionResult CustomerRegister(Customer obj)
 {
     try
     {
         using (ElectricityBillPortalFinalCaseStudyEntities db = new ElectricityBillPortalFinalCaseStudyEntities())
         {
             // Check for existing customer records
             var existingCustomer = db.Customers
                 .FirstOrDefault(c => c.AdhaarNumber == obj.AdhaarNumber ||
                                      c.MobileNumber == obj.MobileNumber ||
                                      c.Username == obj.Username ||
                                      c.Email == obj.Email);

             if (existingCustomer != null)
             {
                 // Set validation messages
                 if (existingCustomer.AdhaarNumber == obj.AdhaarNumber)
                     ModelState.AddModelError("AdhaarNumber", "Aadhaar number already taken.");
                 if (existingCustomer.MobileNumber == obj.MobileNumber)
                     ModelState.AddModelError("MobileNumber", "Mobile number already taken.");
                 if (existingCustomer.Username == obj.Username)
                     ModelState.AddModelError("Username", "Username already taken.");
                 if (existingCustomer.Email == obj.Email)
                     ModelState.AddModelError("Email", "Email already taken.");

                 return View(obj);
             }

             // If no existing records, proceed to save
             obj.RegistrationDate = DateTime.Now;
             db.Customers.Add(obj);
             db.SaveChanges();
             TempData["SuccessMessage"] = "Saved successfully";
             return RedirectToAction("Login");
         }
     }
     catch (Exception ex)
     {
         TempData["ErrorMessage"] = "An error occurred while saving the customer: " + ex.Message;
         return View(obj);
     }
 }
